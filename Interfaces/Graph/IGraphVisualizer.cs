namespace Interfaces;
    public interface IGraphVisualizer
    {
        public SvgDocument visualize(IGraph<T> graph);
        //         ^
        //   subject to change
    }
