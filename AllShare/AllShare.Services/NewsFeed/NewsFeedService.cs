using System;
using System.Collections.Generic;
using AllShare.Core.Domain;
using AllShare.Core.Repositories;
using AllShare.Services.DTOs;
using AllShare.Services.Utils;
using Nelibur.ObjectMapper;

namespace AllShare.Services.NewsFeed
{
    public class NewsFeedService: INewsFeedService
    {
        private IPostRepository PostRepository { get; set; }
        public NewsFeedService(IPostRepository postRepository)
        {
            PostRepository = postRepository;
        }

        public ServiceResult<PostDTO> AddPost(PostDTO dto)
        {
            try
            {
                var post = PostRepository.Add(TinyMapper.Map<Post>(dto));
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
                var posts = PostRepository.GetAll();
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
