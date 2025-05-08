namespace API.Data;

using API.Controllers;
using API.DataEntities;
 

using API.DTOs;
using API.Helpers;

public interface ILikesRepository
 

{
 

    public Task<UserLike?> GetUserLikeAsync(int sourceUserId, int targerUserId);
 

    public Task<IEnumerable<MemberResponse>> GetUserLikesAsync(string predicate, int userId);
 

    public Task<IEnumerable<int>> GetCurrentUserLikeIdsAsync(int currentUSerId);
 

    public void RemoveLike(UserLike like); 

    public void AddLike(UserLike like);
 

    public Task<bool> SaveChangesAsync();
        public Task<PagedList<MemberResponse>> GetUserLikesAsync(LikesParams likesParams);
 


 

}