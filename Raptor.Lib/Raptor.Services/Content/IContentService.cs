using Raptor.Data.Models.Content;
using System.Collections.Generic;

namespace Raptor.Services.Content
{
    public interface IContentService
    {
        #region Terms

        /// <summary>
        /// Create a new term
        /// </summary>
        /// <param name="name">Name of the term</param>
        /// <param name="slug">Slug of the term</param>
        /// <returns>Newly created term object</returns>
        Term CreateTerm(string name, string slug = "");

        /// <summary>
        /// Update an existing term
        /// </summary>
        /// <param name="term">Term to be updated</param>
        void UpdateTerm(Term term);

        /// <summary>
        /// Delete a term
        /// </summary>
        /// <param name="termId">Id of the term to be deleted</param>
        void DeleteTerm(int termId);

        /// <summary>
        /// Delete a term
        /// </summary>
        /// <param name="term">Term object to be deleted</param>
        void DeleteTerm(Term term);

        /// <summary>
        /// Returns a list of terms
        /// </summary>
        /// <returns>A list of all terms</returns>
        IList<Term> GetAllTerms();

        #endregion

        #region Taxonomy

        /// <summary>
        /// Creates a new taxonomy
        /// </summary>
        /// <param name="termId">Id of the term associated with the taxonomy</param>
        /// <param name="name">Name of the taxonomy</param>
        /// <param name="description">Description of the taxonomy</param>
        /// <returns>Newly created taxonomy object</returns>
        Taxonomy CreateTaxonomy(int termId, string name, string description);

        /// <summary>
        /// Creates a new taxonomy
        /// </summary>
        /// <param name="taxonomy">Taxonomy object to be created</param>
        void CreateTaxonomy(Taxonomy taxonomy);

        /// <summary>
        /// Update an existing taxonomy objext
        /// </summary>
        /// <param name="taxonomy">Taxonomy object to update</param>
        void UpdateTaxonomy(Taxonomy taxonomy);

        /// <summary>
        /// Increase the taxonomy count
        /// </summary>
        /// <param name="taxonomyId">Id of the taxonomy</param>
        void IncrementTaxonomyCount(int taxonomyId);

        /// <summary>
        /// Decrease the taxonomy count
        /// </summary>
        /// <param name="taxonomyId">Id of the taxonomy</param>
        void DecrementTaxonomyCount(int taxonomyId);

        /// <summary>
        /// Delete a taxonomy
        /// </summary>
        /// <param name="taxonomy">Taxonomy to be deleted</param>
        void DeleteTaxonomy(Taxonomy taxonomy);

        /// <summary>
        /// Delete a taxonomy by id
        /// </summary>
        /// <param name="taxonomyId">Id of the taxonomy to be deleted</param>
        void DeleteTaxonomy(int taxonomyId);

        #endregion
    }
}
