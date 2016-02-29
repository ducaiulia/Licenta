using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllShare.Services.DTOs;

namespace AllShare.Services.NewsFeed
{
    public interface INewsFeedService
    {
        ServiceResult<PostDTO> AddPost(PostDTO dto);
        ServiceResult<IList<PostDTO>> GetAllPosts();
    }
}
