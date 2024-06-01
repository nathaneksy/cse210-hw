using System;
using System.Collections.Generic;

namespace YouTubeVideoTracker
{
    public class Comment
    {
        public string CommenterName { get; set; }
        public string CommentText { get; set; }

        public Comment(string commenterName, string commentText)
        {
            CommenterName = commenterName;
            CommentText = commentText;
        }
    }
    public class Video
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int LengthInSeconds { get; set; }
        private List<Comment> Comments { get; set; }

        public Video(string title, string author, int lengthInSeconds)
        {
            Title = title;
            Author = author;
            LengthInSeconds = lengthInSeconds;
            Comments = new List<Comment>();
        }

        public void AddComment(Comment comment)
        {
            Comments.Add(comment);
        }

        public int GetNumberOfComments()
        {
            return Comments.Count;
        }

        public List<Comment> GetComments()
        {
            return Comments;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var video1 = new Video("Heres how you can learn CS in 10 minutes!", "CodingWithNate", 600);
            var video2 = new Video("Advanced CS tutorial for pros", "TechWithNate", 1200);
            var video3 = new Video("CS core ideas and how to implement them.", "CodingMaster", 1500);

            video1.AddComment(new Comment("Alice", "I know how to code CS now!"));
            video1.AddComment(new Comment("Bob", "I love this video, straight to the point, nice job."));
            video1.AddComment(new Comment("Charlie", "Very well explained, thank you!"));
            video1.AddComment(new Comment("Greg", "What other languages do you reccoment I learn?"));

            video2.AddComment(new Comment("David", "Great video Nate!"));
            video2.AddComment(new Comment("Eve", "Loved the examples, do you have more videos about this?"));
            video2.AddComment(new Comment("Frank", "This was exactly what I needed to learn."));

            video3.AddComment(new Comment("Grace", "Good explanation of patterns but how can I implement them into my program?"));
            video3.AddComment(new Comment("Hannah", "Clear and concise, straight to the point! Just what I needed."));
            video3.AddComment(new Comment("Ian", "Awesome video man!"));

            var videos = new List<Video> { video1, video2, video3 };

            foreach (var video in videos)
            {
                Console.WriteLine($"Title: {video.Title}");
                Console.WriteLine($"Author: {video.Author}");
                Console.WriteLine($"Length: {video.LengthInSeconds} seconds");
                Console.WriteLine($"Number of comments: {video.GetNumberOfComments()}");

                foreach (var comment in video.GetComments())
                {
                    Console.WriteLine($"- {comment.CommenterName}: {comment.CommentText}");
                }

                Console.WriteLine();
            }
        }
    }
}
