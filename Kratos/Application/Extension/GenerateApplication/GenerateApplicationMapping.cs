using Core.Entities;
using System.Text;

namespace Application.Extension.GenerateApplication;

public class GenerateApplicationMapping
{
    public static string GenerateCodeApplicationMapping(string convertClassForSingle)
    {
        var stringBuildeMapping = new StringBuilder();


        stringBuildeMapping.AppendLine("////// Camda de application > dentro da classe AutoMapperConfiguration");
        stringBuildeMapping.AppendLine();

        stringBuildeMapping.AppendLine($"using Application.Queries.{convertClassForSingle}.GetAll;");
        stringBuildeMapping.AppendLine($"using Application.Queries.{convertClassForSingle}.GetById;");
        stringBuildeMapping.AppendLine();

        stringBuildeMapping.AppendLine($" #region {convertClassForSingle}");
        stringBuildeMapping.AppendLine($"  CreateMap<{convertClassForSingle}, Create{convertClassForSingle}CommandRequest>().ReverseMap();");
        stringBuildeMapping.AppendLine($"  CreateMap<{convertClassForSingle}, Update{convertClassForSingle}CommandRequest>().ReverseMap();");
        stringBuildeMapping.AppendLine();

        stringBuildeMapping.AppendLine($"CreateMap<{convertClassForSingle}, Query{convertClassForSingle}GetAllResponse>().ReverseMap();");
        stringBuildeMapping.AppendLine($"CreateMap<{convertClassForSingle}, Query{convertClassForSingle}GetByIdResponse>().ReverseMap();");
        stringBuildeMapping.AppendLine($"#endregion");

        return stringBuildeMapping.ToString();
    }
}
