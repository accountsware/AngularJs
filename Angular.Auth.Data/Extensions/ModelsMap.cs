/*
 * Copyright 2014 Dominick Baier, Brock Allen
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
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Angular.AuthInfrastructure.Models;
using AutoMapper;

namespace Angular.AuthInfrastructure.Models
{
    public static class EntitiesMap
    {
        static EntitiesMap()
        {
            Mapper.CreateMap<Scope, Angular.Auth.Data.Entities.Scope>(MemberList.Source)
                .ForSourceMember(x => x.Claims, opts => opts.Ignore())
                .ForMember(x => x.ScopeClaims, opts => opts.MapFrom(src => src.Claims.Select(x => x)));
            Mapper.CreateMap<ScopeClaim, Angular.Auth.Data.Entities.ScopeClaim>(MemberList.Source);

            Mapper.CreateMap<ClientSecret, Angular.Auth.Data.Entities.ClientSecret>(MemberList.Source);
            Mapper.CreateMap<Client, Angular.Auth.Data.Entities.Client>(MemberList.Source)
                .ForMember(x => x.CustomGrantTypeRestrictions, opt => opt.MapFrom(src => src.CustomGrantTypeRestrictions.Select(x => new Angular.Auth.Data.Entities.ClientGrantTypeRestriction { GrantType = x })))
                .ForMember(x => x.RedirectUris, opt => opt.MapFrom(src => src.RedirectUris.Select(x => new Angular.Auth.Data.Entities.ClientRedirectUri { Uri = x })))
                .ForMember(x => x.PostLogoutRedirectUris, opt => opt.MapFrom(src => src.PostLogoutRedirectUris.Select(x => new Angular.Auth.Data.Entities.ClientPostLogoutRedirectUri { Uri = x })))
                .ForMember(x => x.IdentityProviderRestrictions, opt => opt.MapFrom(src => src.IdentityProviderRestrictions.Select(x => new Angular.Auth.Data.Entities.ClientIdPRestriction { Provider = x })))
                .ForMember(x => x.ScopeRestrictions, opt => opt.MapFrom(src => src.ScopeRestrictions.Select(x => new Angular.Auth.Data.Entities.ClientScopeRestriction { Scope = x })))
                .ForMember(x => x.AllowedCorsOrigins, opt => opt.MapFrom(src => src.AllowedCorsOrigins.Select(x => new Angular.Auth.Data.Entities.ClientCorsOrigin { Origin = x })))
                .ForMember(x => x.Claims, opt => opt.MapFrom(src => src.Claims.Select(x => new Angular.Auth.Data.Entities.ClientClaim { Type = x.Type, Value = x.Value })));

            Mapper.AssertConfigurationIsValid();
        }

        public static Angular.Auth.Data.Entities.Scope ToEntity(this Scope s)
        {
            if (s == null) return null;

            if (s.Claims == null)
            {
                s.Claims = new List<ScopeClaim>();
            }

            return Mapper.Map<Scope, Angular.Auth.Data.Entities.Scope>(s);
        }

        public static Angular.Auth.Data.Entities.Client ToEntity(this Client s)
        {
            if (s == null) return null;

            if (s.ClientSecrets == null)
            {
                s.ClientSecrets = new List<ClientSecret>();
            }
            if (s.RedirectUris == null)
            {
                s.RedirectUris = new List<string>();
            }
            if (s.PostLogoutRedirectUris == null)
            {
                s.PostLogoutRedirectUris = new List<string>();
            }
            if (s.ScopeRestrictions == null)
            {
                s.ScopeRestrictions = new List<string>();
            }
            if (s.IdentityProviderRestrictions == null)
            {
                s.IdentityProviderRestrictions = new List<string>();
            }
            if (s.Claims == null)
            {
                s.Claims = new List<Claim>();
            }
            if (s.CustomGrantTypeRestrictions == null)
            {
                s.CustomGrantTypeRestrictions = new List<string>();
            }
            if (s.AllowedCorsOrigins == null)
            {
                s.AllowedCorsOrigins = new List<string>();
            }

            return Mapper.Map<Client, Angular.Auth.Data.Entities.Client>(s);
        }
    }
}
