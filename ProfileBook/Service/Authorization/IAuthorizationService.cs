﻿
namespace ProfileBook.Service.Authorization
{
    public interface IAuthorizationService
    {
        bool IsAuthorized { get; }
        void Unauthorize();

    }
}
