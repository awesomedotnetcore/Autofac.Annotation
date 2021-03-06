﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace Autofac.Annotation
{
    /// <summary>
    /// Configuration file proxy provider that skips loading and provides
    /// contnts from a stream.
    /// </summary>
    /// <typeparam name="TSource">
    /// The type of configuration source that generates a file provider.
    /// </typeparam>
    internal class EmbeddedConfigurationProvider<TSource> : IConfigurationProvider
        where TSource : IConfigurationSource, new()
    {
        private readonly FileConfigurationProvider _provider;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileStream"></param>
        public EmbeddedConfigurationProvider(Stream fileStream)
        {
            var source = new TSource();
            this._provider = source.Build(new ConfigurationBuilder()) as FileConfigurationProvider;
            this._provider.Load(fileStream);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="earlierKeys"></param>
        /// <param name="parentPath"></param>
        /// <returns></returns>
        public IEnumerable<string> GetChildKeys(IEnumerable<string> earlierKeys, string parentPath)
        {
            return this._provider.GetChildKeys(earlierKeys, parentPath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IChangeToken GetReloadToken()
        {
            return this._provider.GetReloadToken();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Load()
        {
            // Do nothing - we load via stream.
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Set(string key, string value)
        {
            this._provider.Set(key, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGet(string key, out string value)
        {
            return this._provider.TryGet(key, out value);
        }
    }
}
