﻿/*
 * Copyright 2014, 2015 Dominick Baier, Brock Allen
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Angular.AuthInfrastructure.App_Packages.LibLog._2._0;
using Angular.AuthInfrastructure.Extensions;
using Angular.AuthInfrastructure.ViewModels;
using Microsoft.Owin;
using Thinktecture.IdentityModel;

#pragma warning disable 1591

namespace Angular.AuthInfrastructure.Configuration.Hosting
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class AntiForgeryToken
    {
        private readonly static ILog Logger = LogProvider.GetCurrentClassLogger();

        const string TokenName = "idsrv.xsrf";
        const string CookieEntropy = TokenName + "AntiForgeryTokenCookie";
        const string HiddenInputEntropy = TokenName + "AntiForgeryTokenHidden";

        readonly IOwinContext context;
        readonly IdentityServerOptions options;

        internal AntiForgeryToken(IOwinContext context, IdentityServerOptions options)
        {
            this.context = context;
            this.options = options;
        }

        internal AntiForgeryTokenViewModel GetAntiForgeryToken()
        {
            var tokenBytes = GetCookieToken();
            var protectedTokenBytes = options.DataProtector.Protect(tokenBytes, HiddenInputEntropy);
            var token = Base64Url.Encode(protectedTokenBytes);

            return new AntiForgeryTokenViewModel
            {
                Name = TokenName,
                Value = token
            };
        }
        
        internal async Task<bool> IsTokenValid()
        {
            try
            {
                var cookieToken = GetCookieToken();
                var hiddenInputToken = await GetHiddenInputTokenAsync();
                return CompareByteArrays(cookieToken, hiddenInputToken);
            }
            catch(Exception ex)
            {
                Logger.ErrorException("AntiForgeryTokenValidator validating token", ex);
            }
            return false;
        }

        [MethodImpl(MethodImplOptions.NoOptimization)]
        bool CompareByteArrays(byte[] cookieToken, byte[] hiddenInputToken)
        {
            if (cookieToken == null || hiddenInputToken == null) return false;
            if (cookieToken.Length != hiddenInputToken.Length) return false;
            
            bool same = true;
            for(var i = 0; i < cookieToken.Length; i++)
            {
                same &= (cookieToken[i] == hiddenInputToken[i]);
            }
            return same;
        }

        byte[] GetCookieToken()
        {
            var cookieName = options.AuthenticationOptions.CookieOptions.Prefix + TokenName;
            var cookie = context.Request.Cookies[cookieName];

            if (cookie != null)
            {
                try
                {
                    var protectedCookieBytes = Base64Url.Decode(cookie);
                    var tokenBytes = options.DataProtector.Unprotect(protectedCookieBytes, CookieEntropy);
                    return tokenBytes;
                }
                catch(Exception ex)
                {
                    // if there's an exception we fall thru the catch block to reissue a new cookie
                    Logger.WarnFormat("Problem unprotecting cookie; Issuing new cookie. Error message: {0}", ex.Message);
                }
            }

            var bytes = CryptoRandom.CreateRandomKey(16);
            var protectedTokenBytes = options.DataProtector.Protect(bytes, CookieEntropy);
            var token = Base64Url.Encode(protectedTokenBytes);
            
            var secure = context.Request.Scheme == Uri.UriSchemeHttps;
            var path = context.Request.Environment.GetIdentityServerBasePath().CleanUrlPath();
            context.Response.Cookies.Append(cookieName, token, new Microsoft.Owin.CookieOptions
            {
                HttpOnly = true,
                Secure = secure,
                Path = path
            });

            return bytes;
        }

        async Task<byte[]> GetHiddenInputTokenAsync()
        {
            // hack to clear a possible cached type from Katana in environment
            context.Environment.Remove("Microsoft.Owin.Form#collection");

            if (!context.Request.Body.CanSeek)
            {
                var copy = new MemoryStream();
                await context.Request.Body.CopyToAsync(copy);
                copy.Seek(0L, SeekOrigin.Begin);
                context.Request.Body = copy;
            }
            var form = await context.Request.ReadFormAsync();
            context.Request.Body.Seek(0L, SeekOrigin.Begin);

            // hack to prevent caching of an internalized type from Katana in environment
            context.Environment.Remove("Microsoft.Owin.Form#collection");

            var token = form[TokenName];
            if (token == null) return null;

            var tokenBytes = Base64Url.Decode(token);
            var unprotectedTokenBytes = options.DataProtector.Unprotect(tokenBytes, HiddenInputEntropy);
            
            return unprotectedTokenBytes;
        }
    }
}
