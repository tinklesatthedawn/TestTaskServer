using Application.UseCases.Register.Create;
using Application.UseCases.RegisterValue.Create;
using Domain.Entities;
using Network.Dto;
using Network.Exceptions;
using Network.Http.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Network.Http.RegisterValue.Operations
{
    internal class CreateOperation : IHttpControllerOperation
    {
        public async Task<HttpControllerResponse> Execute(HttpControllerRequest request, CancellationToken cancellationToken)
        {
            var registerValue = GetFromRequest(request);
            if (registerValue != null)
            {
                CreateRegisterValue usecase = DependencyInjection.DI.Get<CreateRegisterValue>();
                var result = await usecase.Handle(registerValue, cancellationToken);
                var registerValueResponded = RegisterValueDto.Of(result.RegisterValue);
                if (registerValueResponded != null)
                {
                    string json = registerValueResponded.ToJson();
                    return new HttpControllerResponse(System.Net.HttpStatusCode.OK, json);
                }
                throw new EntityFailedCreationException(this, registerValue);
            }
            throw new UnknownErrorException(this);
        }

        private RegisterValueEntity? GetFromRequest(HttpControllerRequest request)
        {
            var registerValue = RegisterValueDto.FromJson(request.Body)?.ToEntity();
            if (registerValue == null)
            {
                var arguments = request.Arguments;
                int registerId;
                string? value;

                value = arguments.Get("Value") ?? throw new QueryInvalidException(this, "Value");
                if (!int.TryParse(arguments.Get("RegisterId"), out registerId)) throw new QueryInvalidException(this, "RegisterId", arguments.Get("RegisterId"));

                RegisterValueEntity entity = new RegisterValueEntity();
                entity.RegisterId = registerId;
                entity.SetValue(value);
                return entity;
            }
            return registerValue;
        }
    }
}
