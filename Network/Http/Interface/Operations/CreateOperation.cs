using Application.UseCases.Interface.Create;
using Domain.Entities;
using Network.Dto;
using Network.Exceptions;
using Network.Http.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace Network.Http.Interface.Operations
{
    internal class CreateOperation : IHttpControllerOperation
    {
        public async Task<HttpControllerResponse> Execute(HttpControllerRequest request, CancellationToken cancellationToken)
        {
            var @interface = GetFromRequest(request);
            if (@interface != null)
            {
                CreateInterface usecase = DependencyInjection.DI.Get<CreateInterface>();
                var result = await usecase.Handle(new Request(@interface), cancellationToken);
                var interfaceResponsed = InterfaceDto.Of(result.@interface);
                if (interfaceResponsed != null)
                {
                    string json = interfaceResponsed.ToJson();
                    return new HttpControllerResponse(System.Net.HttpStatusCode.OK, json);
                }
                throw new EntityFailedCreationException(this, @interface);
            }
            throw new UnknownErrorException(this);
        }

        private InterfaceEntity? GetFromRequest(HttpControllerRequest request)
        {
            var @interface = InterfaceDto.FromJson(request.Body)?.ToEntity();
            if (@interface == null)
            {
                var arguments = request.Arguments;

                string? name;
                string? description;

                name = arguments.Get("Name") ?? throw new QueryInvalidException(this, "Name");
                description = arguments.Get("Description") ?? throw new QueryInvalidException(this, "Description");

                InterfaceEntity entity = new InterfaceEntity();
                entity.SetName(name);
                entity.SetDescription(description);
                return entity;
            }
            return @interface;
        }
    }
}
