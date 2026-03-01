using Application.UseCases.Interface.Create;
using Application.UseCases.Interface.Update;
using Domain.Entities;
using Network.Dto;
using Network.Exceptions;
using Network.Http.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Network.Http.Interface.Operations
{
    internal class UpdateOperation : IHttpControllerOperation
    {
        public async Task<HttpControllerResponse> Execute(HttpControllerRequest request, CancellationToken cancellationToken)
        {
            var @interface = GetFromRequest(request);
            if (@interface != null)
            {
                UpdateInterface usecase = DependencyInjection.DI.Get<UpdateInterface>();
                var result = await usecase.Handle(@interface, cancellationToken);
                var interfaceResponsed = InterfaceDto.Of(result.@interface);
                if (interfaceResponsed != null)
                {
                    string json = interfaceResponsed.ToJson();
                    return new HttpControllerResponse(System.Net.HttpStatusCode.OK, json);
                }
                throw new EntityNotExistException(this, @interface);
            }
            throw new UnknownErrorException(this);
        }

        private InterfaceEntity? GetFromRequest(HttpControllerRequest request)
        {
            var @interface = InterfaceDto.FromJson(request.Body)?.ToEntity();
            if (@interface == null)
            {
                var arguments = request.Arguments;
                int id;
                string? name;
                string? description;

                name = arguments.Get("Name") ?? throw new QueryInvalidException(this, "Name");
                description = arguments.Get("Description") ?? throw new QueryInvalidException(this, "Description");
                if (!int.TryParse(arguments["Id"], out id)) throw new QueryInvalidException(this, "Id", arguments["Id"]);

                InterfaceEntity entity = new InterfaceEntity();
                entity.Id = id;
                entity.SetName(name);
                entity.SetDescription(description);
                return entity;
            }
            return @interface;
        }
    }
}
