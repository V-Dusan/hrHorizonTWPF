using System.Threading.Tasks;

namespace hrHorizonT.UI.ViewModel
{
    public interface IFriendDetailViewModel
    {
        Task LoadAsync(int friendId);
    }
}