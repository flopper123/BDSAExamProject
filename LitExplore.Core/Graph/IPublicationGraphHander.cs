namespace LitExplore.Core.Graph
{
    public interface IPublicationGraphHander 
    {
        public IGraph<PublicationDto> GetGraph();
        public void DefineRoot(string Title);
    }
}