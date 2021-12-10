namespace LitExplore.Core.Graph
{
    public class PublicationGraphHandeler : IPublicationGraphHander
    {
        IGraph<PublicationDto> _Graph;
        IPublicationRepository _repo;

        public PublicationGraphHandeler(IPublicationRepository repo)
        {
            _Graph = new Graph<PublicationDto>();
            _repo = repo;
        }
        public IGraph<PublicationDto> GetGraph()
        {
            return _Graph;
        }
        public void DefineRoot(string Title)
        {
            PublicationDto root = _repo.ReadAsync(Title).GetAwaiter().GetResult();
            IVertex<PublicationDto> parrentVertex = new Vertex<PublicationDto>(root!);
            addChildrenToGraph(root, parrentVertex);

        }
        private void addChildrenToGraph(PublicationDto parrent, IVertex<PublicationDto> parrentVertex)
        {
            if (_Graph.Contains(parrent))
            {
                return;
            }
            _Graph.Add(parrentVertex);
            foreach (var childTitle in parrent.References)
            {
                PublicationDto child = _repo.ReadAsync(childTitle.Title).GetAwaiter().GetResult();
                IVertex<PublicationDto> childVertex = new Vertex<PublicationDto>(child, parrentVertex);
                addChildrenToGraph(child, childVertex);
            }
        }
    }
}