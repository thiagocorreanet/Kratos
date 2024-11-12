using Core.Entities;
using System.Text;

namespace Application.Extension.GenerateCore;

public class GenerateInterfaceRepository
{
    public static string GenerateCodeInterfaceRepository(Entity getEntities, string convertClassForSingle)
    {
        var stringBuilderInterfaceRepository = new StringBuilder();

        stringBuilderInterfaceRepository.AppendLine("////// Repositories");
        stringBuilderInterfaceRepository.AppendLine();
        stringBuilderInterfaceRepository.AppendLine("using Core.Entities;");
        stringBuilderInterfaceRepository.AppendLine();

        stringBuilderInterfaceRepository.AppendLine("namespace Core.Repositories;");
        stringBuilderInterfaceRepository.AppendLine();

        stringBuilderInterfaceRepository.AppendLine($"public interface I{convertClassForSingle}Repository : IBaseRepository<{convertClassForSingle}>");
        stringBuilderInterfaceRepository.AppendLine("{}");

        return stringBuilderInterfaceRepository.ToString();
    }
}
