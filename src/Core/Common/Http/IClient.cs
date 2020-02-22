﻿using System;
using System.Threading.Tasks;

namespace Optivem.Atomiv.Core.Common.Http
{
    public interface IClient
    {
        Task<IClientResponse> GetAsync(string uri, string acceptType);

        Task<IClientResponse> PostAsync(string uri, string content, string contentType, string acceptType);

        Task<IClientResponse> PutAsync(string uri, string content, string contentType, string acceptType);

        Task<IClientResponse> DeleteAsync(string uri, string acceptType);
    }
}