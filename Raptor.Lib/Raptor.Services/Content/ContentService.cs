using Raptor.Data.Core;
using Raptor.Data.Models.Content;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Raptor.Services.Content
{
    public class ContentService : IContentService
    {
        private readonly IRepository<Term> _termRepository;
        private readonly IRepository<Taxonomy> _taxonomyRepository;
        private readonly IRepository<TermRelationship> _termRelationshipRepository;

        public ContentService(IRepository<Term> termRepository, IRepository<Taxonomy> taxonomyRepository, IRepository<TermRelationship> termRelationshipRepository) {
            _termRepository = termRepository;
            _taxonomyRepository = taxonomyRepository;
            _termRelationshipRepository = termRelationshipRepository;
        }

        #region Terms

        /// <summary>
        /// Create a new term
        /// </summary>
        /// <param name="name">Name of the term</param>
        /// <param name="slug">Slug of the term</param>
        /// <returns>Newly created term object</returns>
        public Term CreateTerm(string name, string slug = "") {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name of the term cannot be empty.", nameof(name));

            var term = new Term() {
                Name = name,
                Slug = slug
            };

            _termRepository.Create(term);

            return term;
        }

        /// <summary>
        /// Update an existing term
        /// </summary>
        /// <param name="term">Term to be updated</param>
        public void UpdateTerm(Term term) {
            if (term == null)
                throw new ArgumentException("Term cannot be null.", nameof(term));

            _termRepository.Update(term);
        }

        /// <summary>
        /// Delete a term
        /// </summary>
        /// <param name="termId">Id of the term to be deleted</param>
        public void DeleteTerm(int termId) {
            if (termId == 0)
                throw new ArgumentException("Term Id cannot be zero.", nameof(termId));

            var term = _termRepository.GetById(termId);

            if (term == null)
                throw new ArgumentException($"No term found for the specified id = {termId}", nameof(term));

            _termRepository.Delete(term);
        }

        /// <summary>
        /// Delete a term
        /// </summary>
        /// <param name="term">Term object to be deleted</param>
        public void DeleteTerm(Term term) {
            if (term == null)
                throw new ArgumentException("Term cannot be null.", nameof(term));

            _termRepository.Delete(term);
        }

        /// <summary>
        /// Returns a list of terms
        /// </summary>
        /// <returns>A list of all terms</returns>
        public IList<Term> GetAllTerms() {
            return _termRepository.GetAll().ToList();
        }

        #endregion

        #region Taxonomy

        /// <summary>
        /// Creates a new taxonomy
        /// </summary>
        /// <param name="termId">Id of the term associated with the taxonomy</param>
        /// <param name="name">Name of the taxonomy</param>
        /// <param name="description">Description of the taxonomy</param>
        /// <returns>Newly created taxonomy object</returns>
        public Taxonomy CreateTaxonomy(int termId, string name, string description = "") {
            if (termId == 0)
                throw new ArgumentException("Term Id cannot be zero.", nameof(termId));

            var term = _termRepository.GetById(termId);

            if (term == null)
                throw new ArgumentException($"No term found for the specified id = {termId}", nameof(term));

            var taxonomy = new Taxonomy() {
                TermId = termId,
                Name = name,
                Description = description,
                Count = 1
            };

            _taxonomyRepository.Create(taxonomy);

            return taxonomy;

        }

        /// <summary>
        /// Creates a new taxonomy
        /// </summary>
        /// <param name="taxonomy">Taxonomy object to be created</param>
        public void CreateTaxonomy(Taxonomy taxonomy) {
            if (taxonomy.TermId == 0)
                throw new ArgumentException("Term Id cannot be zero.", nameof(taxonomy.TermId));

            var term = _termRepository.GetById(taxonomy.TermId);

            if (term == null)
                throw new ArgumentException($"No term found for the specified id = {taxonomy.TermId}", nameof(term));

            _taxonomyRepository.Create(taxonomy);
        }

        /// <summary>
        /// Update an existing taxonomy objext
        /// </summary>
        /// <param name="taxonomy">Taxonomy object to update</param>
        public void UpdateTaxonomy(Taxonomy taxonomy) {
            if (taxonomy == null)
                throw new ArgumentException("Taxonomy cannot be null.", nameof(taxonomy));

            _taxonomyRepository.Update(taxonomy);
        }

        /// <summary>
        /// Increase the taxonomy count
        /// </summary>
        /// <param name="taxonomyId">Id of the taxonomy</param>
        public void IncrementTaxonomyCount(int taxonomyId) {
            if (taxonomyId == 0)
                throw new ArgumentException("Taxonomy Id cannot be zero.", nameof(taxonomyId));

            var taxonomy = _taxonomyRepository.GetById(taxonomyId);
            if (taxonomy == null)
                throw new ArgumentException($"No taxonomy found for specified id = {taxonomyId}.", nameof(taxonomyId));

            taxonomy.Count++;
            _taxonomyRepository.Update(taxonomy);
        }

        /// <summary>
        /// Decrease the taxonomy count
        /// </summary>
        /// <param name="taxonomyId">Id of the taxonomy</param>
        public void DecrementTaxonomyCount(int taxonomyId) {
            if (taxonomyId == 0)
                throw new ArgumentException("Taxonomy Id cannot be zero.", nameof(taxonomyId));

            var taxonomy = _taxonomyRepository.GetById(taxonomyId);
            if (taxonomy == null)
                throw new ArgumentException($"No taxonomy found for specified id = {taxonomyId}.", nameof(taxonomyId));

            taxonomy.Count--;
            _taxonomyRepository.Update(taxonomy);
        }

        /// <summary>
        /// Delete a taxonomy
        /// </summary>
        /// <param name="taxonomy">Taxonomy to be deleted</param>
        public void DeleteTaxonomy(Taxonomy taxonomy) {
            if (taxonomy == null)
                throw new ArgumentException("Taxonomy cannot be null.", nameof(taxonomy));

            _taxonomyRepository.Delete(taxonomy);
        }

        /// <summary>
        /// Delete a taxonomy by id
        /// </summary>
        /// <param name="taxonomyId">Id of the taxonomy to be deleted</param>
        public void DeleteTaxonomy(int taxonomyId) {
            if (taxonomyId == 0)
                throw new ArgumentException("Taxonomy Id cannot be zero.", nameof(taxonomyId));

            var taxonomy = _taxonomyRepository.GetById(taxonomyId);
            if (taxonomy == null)
                throw new ArgumentException($"No taxonomy found for specified id = {taxonomyId}.", nameof(taxonomyId));

            _taxonomyRepository.Delete(taxonomy);
        }

        #endregion
    }
}
