using System.Threading.Tasks;

namespace BiharEnergy.Core.Domain.IssueNoteModule
{
    public interface IIssueNoteRepository : IRepositoryBase<IssueNote>
    {
        Task<int> GetLastIssueNoteNumber(string tenantId);
        Task<bool> IsIssueNoteNumberUnique(string tenantId, int issueNoteNumber);


    }
}