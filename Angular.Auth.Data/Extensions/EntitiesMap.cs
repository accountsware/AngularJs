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

using System.Linq;
using System.Security.Claims;
using AutoMapper;

namespace Angular.Auth.Data.Extensions
{
    public static class EntitiesMap
    {
        static EntitiesMap()
        {
            Mapper.CreateMap<Angular.Auth.Data.Entities.Scope, Angular.AuthInfrastructure.Models.Scope>(MemberList.Destination)
                .ForMember(x => x.Claims, opts => opts.MapFrom(src => src.ScopeClaims.Select(x => x)));
            Mapper.CreateMap<Angular.Auth.Data.Entities.ScopeClaim, Angular.AuthInfrastructure.Models.ScopeClaim>(MemberList.Destination);

            Mapper.CreateMap<Angular.Auth.Data.Entities.ClientSecret, Angular.AuthInfrastructure.Models.ClientSecret>(MemberList.Destination);
            Mapper.CreateMap<Angular.Auth.Data.Entities.Client, Angular.AuthInfrastructure.Models.Client>(MemberList.Destination)
                .ForMember(x => x.CustomGrantTypeRestrictions, opt => opt.MapFrom(src => src.CustomGrantTypeRestrictions.Select(x => x.GrantType)))
                .ForMember(x => x.RedirectUris, opt => opt.MapFrom(src => src.RedirectUris.Select(x => x.Uri)))
                .ForMember(x => x.PostLogoutRedirectUris, opt => opt.MapFrom(src => src.PostLogoutRedirectUris.Select(x => x.Uri)))
                .ForMember(x => x.IdentityProviderRestrictions, opt => opt.MapFrom(src => src.IdentityProviderRestrictions.Select(x => x.Provider)))
                .ForMember(x => x.ScopeRestrictions, opt => opt.MapFrom(src => src.ScopeRestrictions.Select(x => x.Scope)))
                .ForMember(x => x.AllowedCorsOrigins, opt => opt.MapFrom(src => src.AllowedCorsOrigins.Select(x => x.Origin)))
                .ForMember(x => x.Claims, opt => opt.MapFrom(src => src.Claims.Select(x => new Claim(x.Type, x.Value))));

            Mapper.AssertConfigurationIsValid();
        }

        public static Angular.AuthInfrastructure.Models.Scope ToModel(this Angular.Auth.Data.Entities.Scope s)
        {
            if (s == null) return null;
            return Mapper.Map<Angular.Auth.Data.Entities.Scope, Angular.AuthInfrastructure.Models.Scope>(s);
        }

        public static Angular.AuthInfrastructure.Models.Client ToModel(this Angular.Auth.Data.Entities.Client s)
        {
            if (s == null) return null;
            return Mapper.Map<Angular.Auth.Data.Entities.Client, Angular.AuthInfrastructure.Models.Client>(s);
        }
    }
}
