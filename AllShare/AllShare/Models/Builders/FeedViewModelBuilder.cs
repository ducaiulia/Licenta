using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AllShare.Services.NewsFeed;
using AllShare.Services.Utils;
using Nelibur.ObjectMapper;

namespace AllShare.Models.Builders
{
    public class FeedViewModelBuilder: IViewModelBuilder<NewsFeedViewModel>
    {
        private INewsFeedService _newsFeedService { get; set; }

        public FeedViewModelBuilder(INewsFeedService newsFeedService)
        {
            _newsFeedService = newsFeedService;
        }

        public NewsFeedViewModel Build()
        {
            var viewModel = new NewsFeedViewModel();
            var result = _newsFeedService.GetAllPosts();
            if (result.ResultType == ResultType.Success)
            {
                viewModel.Posts = new List<PostViewModel>();
                foreach (var post in result.Result)
                {
                    viewModel.Posts.Add(TinyMapper.Map<PostViewModel>(post));
                }
                return viewModel;
            }
            return viewModel;
        }
    }
}