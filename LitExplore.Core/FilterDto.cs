namespace LitExplore.Core;

using System.ComponentModel.DataAnnotations;

public record FilterSequenceDto
{
    // Ordered by this_index 
    public IList<SingleFilterDto> History { get; init; } = null!;
}

public record SingleFilterDto
{
    int this_index;
    string Title;
    IList<(int index, Object? args)> arg_seq;
}

/*
    Depth
        CreateTable(
                "dbo.People",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true), 
                        Name = c.String()
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.Name); // Create an index

*/
    