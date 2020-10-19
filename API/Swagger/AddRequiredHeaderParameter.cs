using Microsoft.Net.Http.Headers;
using NSwag;
using NSwag.Generation.Processors;
using NSwag.Generation.Processors.Contexts;
using NJsonSchema;

namespace API.Swagger
{
    /// <summary>
    /// Add Required Header Parameter
    /// </summary>
    /// <seealso cref="IOperationProcessor" />
    public class AddRequiredHeaderParameter : IOperationProcessor
    {
        /// <summary>
        /// Processes the specified method information.
        /// </summary>
        /// <param name="context">The processor context.</param>
        /// <returns>
        /// true if the operation should be added to the Swagger specification.
        /// </returns>
        public bool Process(OperationProcessorContext context)
        {
            context.OperationDescription.Operation.Parameters.Add(
                new OpenApiParameter
                {
                    Name = HeaderNames.Authorization,
                    Kind = OpenApiParameterKind.Header,
                    Schema = new JsonSchema { Type = JsonObjectType.String },
                    //IsRequired = context.ControllerType != typeof(UserController),
                    Description = "The authorization token (the value should look like \"Bearer accessToken\")"
                });

            return true;
        }
    }
}
