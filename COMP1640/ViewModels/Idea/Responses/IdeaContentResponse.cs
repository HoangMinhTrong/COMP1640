﻿namespace COMP1640.ViewModels.Idea.Responses
{
	public class IdeaContentResponse
	{
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Department { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UserRole { get; set; }
        public int LikeCount { get; set; }
        public int DislikeCount { get; set; }
        public int CommentCount { get; set; }
        public string Category { get; set; }
        public bool IsAnomymous { get; set; }
    }
}
