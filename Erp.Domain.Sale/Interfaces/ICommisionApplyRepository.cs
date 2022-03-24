using Erp.Domain.Sale.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Sale.Interfaces
{
    public interface ICommisionApplyRepository
    {
        /// <summary>
        /// Get all CommisionApply
        /// </summary>
        /// <returns>CommisionApply list</returns>
        IQueryable<CommisionApply> GetAllCommisionApply();
        List<CommisionApply> GetlistAllCommisionApply();

        /// <summary>
        /// Get CommisionApply information by specific id
        /// </summary>
        /// <param name="Id">Id of CommisionApply</param>
        /// <returns></returns>
        CommisionApply GetCommisionApplyById(int Id);

        List<CommisionApply> GetListCommisionApplyByIdCus(int Id);


        IQueryable<CommisionApply> GetAllCommisionApplybyIDCus(int idcus);


        /// <summary>
        /// Insert CommisionApply into database
        /// </summary>
        /// <param name="CommisionApply">Object infomation</param>
        void InsertCommisionApply(CommisionApply CommisionApply);

        /// <summary>
        /// Delete CommisionApply with specific id
        /// </summary>
        /// <param name="Id">CommisionApply Id</param>
        void DeleteCommisionApply(int Id);

        /// <summary>
        /// Delete a CommisionApply with its Id and Update IsDeleted IF that Commision has relationship with others
        /// </summary>
        /// <param name="Id">Id of CommisionApply</param>
        void DeleteCommisionApplyRs(int Id);

        /// <summary>
        /// Update Commision into database
        /// </summary>
        /// <param name="CommisionApply">CommisionApply object</param>
        void UpdateCommisionApply(CommisionApply CommisionApply);

        void DeleteAllCommisionApplyByIdCus(int IdCus);
    }
}
