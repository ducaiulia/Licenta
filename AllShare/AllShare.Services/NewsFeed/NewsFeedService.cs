using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllShare.Core.Domain;
using AllShare.Core.Repositories;
using AllShare.Services.DTOs;
using AllShare.Services.Utils;
using Nelibur.ObjectMapper;

namespace AllShare.Services.NewsFeed
{
    public class NewsFeedService: INewsFeedService
    {
        private IPostRepository _postRepository { get; set; }
        public NewsFeedService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public ServiceResult<PostDTO> AddPost(PostDTO dto)
        {
            try
            {
                var post = _postRepository.Add(TinyMapper.Map<Post>(dto));
                return new ServiceResult<PostDTO> {Result = TinyMapper.Map<PostDTO>(post), ResultType = ResultType.Success};
            }
            catch (Exception ex)
            {
                return new ServiceResult<PostDTO> { ResultType = ResultType.Success };
            }
        }

        public ServiceResult<IList<PostDTO>> GetAllPosts()
        {
            try
            {
                var posts = _postRepository.GetAll();
                var dto = TinyMapper.Map<List<PostDTO>>(posts);
                return new ServiceResult<IList<PostDTO>> {Result = dto, ResultType = ResultType.Success};
            }
            catch (Exception ex)
            {
                return new ServiceResult<IList<PostDTO>> { ResultType = ResultType.Error };
            }
        }
    }
}
