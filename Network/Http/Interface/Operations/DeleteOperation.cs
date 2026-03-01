using Application.UseCases.Interface.Delete;
using Domain.Entities;
using Network.Dto;
using Network.Exceptions;
using Network.Http.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Network.Http.Interface.Operations
{
    internal class DeleteOperation : IHttpControllerOperation
    {
        public async Task<HttpControllerResponse> Execute(HttpControllerRequest request, CancellationToken cancellationToken)
        {
            var @interface = GetFromRequest(request);
            if (@interface != null)
            {
                DeleteInterface usecase = DependencyInjection.DI.Get<DeleteInterface>();
                var result = await usecase.Handle(new Request(@interface), cancellationToken);
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

                if (int.TryParse(arguments.Get("Id"), out id)) throw new QueryInvalidException(this, "Id", arguments.Get("Id"));

                InterfaceEntity entity = new InterfaceEntity();
                entity.Id = id;
                return entity;
            }
            return @interface;
        }
    }
}
