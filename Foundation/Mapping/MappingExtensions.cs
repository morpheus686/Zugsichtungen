using AutoMapper;

namespace Zugsichtungen.Foundation.Mapping
{
    public static class MappingExtensions
    {
        /// <summary>
        /// Wandelt eine Liste von Quellobjekten in eine Liste von Zielobjekten um.
        /// </summary>
        public static List<TDest> MapList<TSource, TDest>(
            this IMapper mapper,
            IEnumerable<TSource> source)
        {
            return [.. source.Select(mapper.Map<TSource, TDest>)];
        }

        /// <summary>
        /// Mappt eine einzelne Entität in das Zielobjekt.
        /// </summary>
        public static TDest MapSingle<TSource, TDest>(
            this IMapper mapper,
            TSource source)
        {
            return mapper.Map<TDest>(source);
        }
    }
}