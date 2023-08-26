using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;
using System.Dynamic;
using System.Text;

public class CustomSlashInputFormatter<T> : TextInputFormatter
{
    public CustomSlashInputFormatter()
    {
        SupportedMediaTypes.Add("application/custom");
    }

    protected override bool CanReadType(Type type)
    {
        return type == typeof(T);
    }

    public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context)
    {
        var request = context.HttpContext.Request;
        using var reader = new StreamReader(request.Body);
        var content = await reader.ReadToEndAsync();

        var lines = content.Split('\n');
        var propertyNames = lines[0].Split('/');
        var propertyValues = lines[1].Split('/');

        var dynamicObject = new ExpandoObject() as IDictionary<string, object>;
        for (var i = 0; i < propertyNames.Length && i < propertyValues.Length; i++)
        {
            dynamicObject[propertyNames[i]] = propertyValues[i];
        }

        var serializer = new Newtonsoft.Json.JsonSerializer();
        using var jsonReader = new JsonTextReader(new StringReader(JsonConvert.SerializeObject(dynamicObject)));
        var product = serializer.Deserialize<T>(jsonReader);

        return await InputFormatterResult.SuccessAsync(product);
    }

    public override Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding encoding)
    {
        throw new NotImplementedException();
    }
}