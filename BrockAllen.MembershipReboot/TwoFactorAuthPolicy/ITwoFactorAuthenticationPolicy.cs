﻿/*
 * Copyright (c) Brock Allen.  All rights reserved.
 * see license.txt
 */


using Angular.Core.Modals.Identity;

namespace BrockAllen.MembershipReboot
{
    public interface ITwoFactorAuthenticationPolicy
    {
        string GetTwoFactorAuthToken(UserAccount account);
        void IssueTwoFactorAuthToken(UserAccount account, string token);
        void ClearTwoFactorAuthToken(UserAccount account);
    }
}
