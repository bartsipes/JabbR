﻿using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using JabbR.ContentProviders;
using JabbR.Services;
using Newtonsoft.Json.Linq;

namespace JabbR.UploadHandlers
{
    public class ImgurUploadHandler : IUploadHandler
    {
        private readonly IApplicationSettings _settings;

        [ImportingConstructor]
        public ImgurUploadHandler(IApplicationSettings settings)
        {
            _settings = settings;
        }

        public bool IsValid(string fileName, string contentType)
        {
            return !String.IsNullOrEmpty(_settings.ImagurClientId) &&
                   ImageContentProvider.IsValidContentType(contentType);
        }

        public async Task<string> UploadFile(string fileName, string contentType, Stream stream)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Client-ID", _settings.ImagurClientId);

            var content = new StreamContent(stream);

            var response = await client.PostAsync("https://api.imgur.com/3/upload", content);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            JObject obj = JObject.Parse(await response.Content.ReadAsStringAsync());

            return obj["data"].Value<string>("link");
        }
    }
}