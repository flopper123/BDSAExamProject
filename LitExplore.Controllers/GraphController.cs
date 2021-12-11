namespace LitExplore.Controllers;

using LitExplore.Controllers.Graph;

public class GraphController
{
    public GraphController() { }
    
    public async Task<VisualGraph> GetGraphRepresentationAsync() {
        // Fetch graph
        // THIS IS TEST DATA FOR NOW
        var publications = GraphMockData.GetPublications();

        // Find graph relations
        // This will weight publications compared to eachother.
        // And give a guess as to where they should be located on 
        //      a normalized square with (x, y) in range of [0-1]
        var graphRelation = new GraphRelation();
        var relations = graphRelation.GetManyToManyRelations(publications);

        return VisualGraph.FromList(relations);
    }
}