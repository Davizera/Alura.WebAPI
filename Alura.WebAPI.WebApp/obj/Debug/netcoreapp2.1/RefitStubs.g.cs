﻿// <auto-generated />
using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Alura.ListaLeitura.Modelos;
using Lista =  Alura.ListaLeitura.Modelos.ListaLeitura;
using Refit;

/* ******** Hey You! *********
 *
 * This is a generated file, and gets rewritten every time you build the
 * project. If you want to edit it, you need to edit the mustache template
 * in the Refit package */

#pragma warning disable
namespace RefitInternalGenerated
{
    [ExcludeFromCodeCoverage]
    [AttributeUsage (AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Delegate)]
    sealed class PreserveAttribute : Attribute
    {

        //
        // Fields
        //
        public bool AllMembers;

        public bool Conditional;
    }
}
#pragma warning restore

namespace Alura.ListaLeitura.WebApp.HttpClients.Contracts
{
    using RefitInternalGenerated;

    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    [DebuggerNonUserCode]
    [Preserve]
    partial class AutoGeneratedILivrosApiClient : ILivrosApiClient        {
        /// <inheritdoc />
        public HttpClient Client { get; protected set; }
        readonly IRequestBuilder requestBuilder;

        /// <inheritdoc />
        public AutoGeneratedILivrosApiClient(HttpClient client, IRequestBuilder requestBuilder)
        {
            Client = client;
            this.requestBuilder = requestBuilder;
        }

        /// <inheritdoc />
        public virtual Task<LivroApi> RecuperarLivroAsync(int id)
        {
            var arguments = new object[] { id };
            var func = requestBuilder.BuildRestResultFuncForMethod("RecuperarLivroAsync", new Type[] { typeof(int) });
            return (Task<LivroApi>)func(Client, arguments);
        }

        /// <inheritdoc />
        public virtual Task<HttpResponseMessage> RecuperarCapaAsync(int id)
        {
            var arguments = new object[] { id };
            var func = requestBuilder.BuildRestResultFuncForMethod("RecuperarCapaAsync", new Type[] { typeof(int) });
            return (Task<HttpResponseMessage>)func(Client, arguments);
        }

        /// <inheritdoc />
        public virtual Task<Lista> RecuperarListaLivroTipo(string tipo)
        {
            var arguments = new object[] { tipo };
            var func = requestBuilder.BuildRestResultFuncForMethod("RecuperarListaLivroTipo", new Type[] { typeof(string) });
            return (Task<Lista>)func(Client, arguments);
        }

        /// <inheritdoc />
        public virtual Task RemoverLivroAsync(int id)
        {
            var arguments = new object[] { id };
            var func = requestBuilder.BuildRestResultFuncForMethod("RemoverLivroAsync", new Type[] { typeof(int) });
            return (Task)func(Client, arguments);
        }

    }
}