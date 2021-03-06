﻿namespace Angular.AuthInfrastructure.Models
{
    /// <summary>
    /// TokenType
    /// </summary>
    public enum TokenType : short
    {
        AuthorizationCode = 1,
        TokenHandle = 2,
        RefreshToken = 3
    }
}
