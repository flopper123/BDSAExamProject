namespace LitExplore.Entity.Repositories;

public static class Seed
{

  public static async void SeedDB(LitExploreContext _context)
  {
    
    _context.Database.EnsureDeleted();
    _context.Database.EnsureCreated();

    PublicationTitle d1c5a900_0342_463f_89fb_496311cd6559 = new PublicationTitle { Title = "Culture and climate for innovation" };
    _context.Publications.AddRange(
     new Publication
     {
       Title = "GLOBAL SOFTWARE ENGINEERING: CHALLENGES AND SOLUTIONS",
       Author = "Fabio Calefato, Alpana Dubey, Christof Ebert, Paolo Tell",
       References =
      new List<PublicationTitle> {
           new PublicationTitle { Title = "Understanding Barriers to Internal Startups in Large Organizations: Evidence from a Globally Distributed Company" }}
     },
    new Publication
    {
      Title = "Understanding Barriers to Internal Startups in Large Organizations: Evidence from a Globally Distributed Company",
      Author = "Tor Sporsem, Anastasiia Tkalich, Nils Brede Moe, Marius Mikalsen",
      References =
      new List<PublicationTitle> {
           new PublicationTitle { Title = "The 2019 SIM IT Issues and Trends Study"},
            new PublicationTitle { Title = "Global Software Engineering: Challenges and solutions"},
            new PublicationTitle { Title = "Innovation Initiatives in Large Software Companies: A Systematic Mapping Study"},
            new PublicationTitle { Title = "Diagnosing a firm’s internal environment for corporate entrepreneurship"},
            new PublicationTitle { Title = "Lean startup: why large software companies should care"},
            new PublicationTitle { Title = "The lean startup: How today’s entrepreneurs use continuous innovation to create radically successful businesses"},
            new PublicationTitle { Title = "Understanding coordination in global software engineering: A mixed-methods study on the use of meetings and Slack"},
            new PublicationTitle { Title = "Corporate entrepreneurship:: antidote or oxymoron?"},
            d1c5a900_0342_463f_89fb_496311cd6559,
            new PublicationTitle { Title = "Large-Scale Agile Transformation: A Case Study of Transforming Business Development and Operations"},
            new PublicationTitle { Title = "Product innovation product–market competition and persistent profitability in the U.S. pharmaceutical industry"},
            new PublicationTitle { Title = "Continuous software engineering: A roadmap and agenda"},
            new PublicationTitle { Title = "Market incumbency and technological inertia"},
            new PublicationTitle { Title = "Building blocks for continuous experimentation"},
            new PublicationTitle { Title = "Lean Internal Startups for Software Product Innovation in Large Companies: Enablers and Inhibitors"},
            new PublicationTitle { Title = "Fostering and Sustaining Innovation in a Fast Growing Agile Company"},
            new PublicationTitle { Title = "The Design Sprint"},
            new PublicationTitle { Title = "From MVPs to pivots: a hypothesis-driven journey of two software startups"},
            new PublicationTitle { Title = "Innovation Speed: A Conceptual Model of Context Antecedents and Outcomes"},
            new PublicationTitle { Title = "Devops: A software revolution in the making"},
            new PublicationTitle { Title = "The Identification and Classification of Impedimen"},
            new PublicationTitle { Title = "Examining the concept of temporality in Information System Development Flow"},
            new PublicationTitle { Title = "Continuous Software Testing in a Globally Distributed Project"},
            new PublicationTitle { Title = "Enabling Knowledge Sharing in Agile Virtual Teams"},
            new PublicationTitle { Title = "Team-external coordination in large-scale software development projects"},
            new PublicationTitle { Title = "Software teams and their knowledge networks in large-scale software development"},
            new PublicationTitle { Title = "Guidelines for conducting and reporting case study research in software engineering"},
            new PublicationTitle { Title = "Challenges of shared decision-making: A multiple case study of agile software development"},
            new PublicationTitle { Title = "Linking business and requirements engineering: is solution planning a missing activity in software product companies"},
            new PublicationTitle { Title = "The Role of Champion in Product Innovation"},
            new PublicationTitle { Title = "Data Sourcing and Data Partnerships: Opportunities for IS Sourcing Research"},
            new PublicationTitle { Title = "The Anatomy of a Large-Scale Experimentation Platform"},
            new PublicationTitle { Title = "Implementing Beyond Budgeting: Unlocking the Performance Potential"},
          }
    },
    new Publication
    {
      Title = "The 2019 SIM IT Issues and Trends Study",
      Author = "Leon Kappelman, Vess L. Johnson, Chris Maurer, Katie Guerra, Ephraim McLean, Russell Torres, Mark Snyder, Kevin Kim",
      References =
      new List<PublicationTitle> {}
    },
    new Publication
    {
      Title = "Innovation Initiatives in Large Software Companies: A Systematic Mapping Study",
      Author = "Henry Edison, Xiaofeng Wang, Ronald Jabangwe, Pekka Abrahamsson",
      References =
      new List<PublicationTitle> {
           new PublicationTitle { Title = "The birth and growth of Toshiba’s laptop and notebook computers: a case study in Japanese corporate venturing"},
            new PublicationTitle { Title = "From science to technology to products and profits: superconductivity at general electric and intermagnetics general (1960–1990)"},
            d1c5a900_0342_463f_89fb_496311cd6559,
            new PublicationTitle { Title = "Preparing organisations for employee-driven open innovation"},
            new PublicationTitle { Title = "Corporate venturing deal syndication and innovation: The information exchange paradox"},
            new PublicationTitle { Title = "A conceptual framework for misfit technology commercialization"},
            new PublicationTitle { Title = "Start-ups, spin-offs and internal projects"},
            new PublicationTitle { Title = "Intrapreneurship: construct refinement and cross-cultural validation"},
            new PublicationTitle { Title = "Bootlegging and path dependency"},
            new PublicationTitle { Title = "The rise and fall of the Merlin–Gerin foundry business: a case study in French corporate entrepreneurship"},
            new PublicationTitle { Title = "Firms resources and sustained competitive advantage"},
            new PublicationTitle { Title = "Towards an open R&D system: Internal R&D investment, external knowledge acquisition and innovation performance acquisition and innovation performance"},
            new PublicationTitle { Title = "Entrepreneurship in multinational corporations: the charateristics of subsidiary initiatives"},
            new PublicationTitle { Title = "Corporate entrepreneurship in network organizations: how subsidiary initiative drive internal market efficiency"},
            new PublicationTitle { Title = "Building Products as Innovation Experiment Systems"},
            new PublicationTitle { Title = "Product development: past research, present findings, and future direction"},
            new PublicationTitle { Title = "Managers’ emotional displays and employees’ willingness to act entrepreneurially"},
            new PublicationTitle { Title = "Opportunity spin-offs and necessity spin-offs"},
            new PublicationTitle { Title = "Intraorganizational ecology of strategy making and organizational adaptation: theory and field research"},
            new PublicationTitle { Title = "High-technology spin-offs from government R&D laboratories and research universities"},
            new PublicationTitle { Title = "The incumbent’s curse? incumbency, size and radical product innovation"},
            new PublicationTitle { Title = "Intrapreneurship: what companies should do to develop new products"},
            new PublicationTitle { Title = "The governance and performance of Xerox’s technology spin-off companies"},
            new PublicationTitle { Title = "The industrial dynamics of Open Innovation – Evidence from the transformation of consumer electronicsEntrepreneurial origin, technological knowledge, and the growth of spin-off companies"},
            new PublicationTitle { Title = "Corporate entrepreneurship: State-of-the-art research and a future research agenda"},
            new PublicationTitle { Title = "A conceptual model of entrepreneurship as firm behavior"},
          }
    },
    new Publication
    {
      Title = "Diagnosing a firm's internal environment for corporate entrepreneurship",
      Author = "Donald F.Kuratko, Jeffrey S.Hornsby, Jeffrey G.Covin",
      References =
      new List<PublicationTitle> {
           new PublicationTitle { Title = "Can apple survive without Steve Jobs?"},
            new PublicationTitle { Title = "The new competitive landscape"},
            new PublicationTitle { Title = "Resources, environmental change, and survival: Asymmetric paths of young independent and subsidiary organizations"},
            new PublicationTitle { Title = "How P&G tripled its innovation success rate"},
            new PublicationTitle { Title = "Organizational actions in response to threats and opportunities"},
            new PublicationTitle { Title = "Corporate entrepreneurship and innovation in Silicon Valley: The case of Google, Inc"},
            new PublicationTitle { Title = "Operations management and corporate entrepreneurship: The moderating effect of operations control on the antecedents of corporate entrepreneurial activity in relation to innovation performance"},
            new PublicationTitle { Title = "Building breakthrough businesses within established organizations"},
            new PublicationTitle { Title = "Assessing a measurement of organizational preparedness for corporate entrepreneurship"},
            new PublicationTitle { Title = "Perception of internal factors for corporate entrepreneurship: A comparison of Canadian and U.S. managers"},
            new PublicationTitle { Title = "Managers’ corporate entrepreneurial actions: Examining perception and position"},
            new PublicationTitle { Title = "Middle managers’ perception of the internal environment for corporate entrepreneurship: Assessing a measurement scale"},
            new PublicationTitle { Title = "Conceptualizing corporate entrepreneurship strategy"},
            new PublicationTitle { Title = "The entrepreneurial imperative of the 21st century"},
            new PublicationTitle { Title = "Entrepreneurship: Theory, process, practice"},
            new PublicationTitle { Title = "Innovation acceleration: Transforming organizational thinking"},
            new PublicationTitle { Title = "Managers’ corporate entrepreneurial actions and job satisfaction"},
            new PublicationTitle { Title = "A model of middle level managers’ entrepreneurial behavior"},
            new PublicationTitle { Title = "Improving firm performance through entrepreneurial actions: Acordia's corporate entrepreneurship strategy"},
            new PublicationTitle { Title = "Developing an entrepreneurial assessment instrument for an effective corporate entrepreneurial environment"},
            new PublicationTitle { Title = "Corporate entrepreneurship & innovation"},
            new PublicationTitle { Title = "Corporate entrepreneurship: An empirical look at the innovativeness dimension and its antecedents"},
            new PublicationTitle { Title = "Antecedents of corporate entrepreneurship"},
          }
    }
    );

    await _context.SaveChangesAsync();
  }
}