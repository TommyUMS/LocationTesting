using System;

namespace TestingLocation.Helpers
{
    public class FreshPageModelMapperMod: FreshMvvm.IFreshPageModelMapper
    {
        public string GetPageTypeName(Type pageModelType)
        {
            return pageModelType.AssemblyQualifiedName
                                .Replace("PageModel", "Page")
                                .Replace("ViewModel", "View");
        }
    }
}