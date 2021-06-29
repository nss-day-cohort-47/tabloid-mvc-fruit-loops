using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public class CommentsRepository : BaseRepository, ICommentsRepository
    {
        public CommentsRepository(IConfiguration config) : base(config) { }
        public void AddComment(Comments comment)
        {
            throw new NotImplementedException();
        }

        public List<Comments> GetAllComments()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT id, subject, content, CreateDateTime, UserProfileId, PostId
                                        FROM Comment;";
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Comments> comments = new List<Comments>() { };
                    while (reader.Read() && reader.IsDBNull(reader.GetOrdinal("id")))
                    {
                        Comments comment = new Comments()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            Subject = reader.GetString(reader.GetOrdinal("subject")),
                            Content = reader.GetString(reader.GetOrdinal("content")),
                            CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime")),
                            UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
                            Post = reader.GetInt32(reader.GetOrdinal("PostId")),
                        };
                        comments.Add(comment);

                    }
                    reader.Close();
                    return comments;

                }
            }
        }

        public List<Comments> GetAllPostCommentsById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT id, subject, content, CreateDateTime, UserProfileId, PostId, isdeleted
                                        FROM Comment
                                        WHERE id = @id;";
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Comments> comments = new List<Comments>() { };
                    while (reader.Read())
                    {
                        Comments comment = new Comments()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            Subject = reader.GetString(reader.GetOrdinal("subject")),
                            Content = reader.GetString(reader.GetOrdinal("content")),
                            CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime")),
                            UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
                            Post = reader.GetInt32(reader.GetOrdinal("PostId")),
                        };
                        if (!reader.GetBoolean(reader.GetOrdinal("isDeleted")))
                        {

                            comments.Add(comment);
                        }

                    }
                    reader.Close();
                    return comments;

                }
            }
        }

        public Comments GetCommentById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT id, subject, content, CreateDateTime, UserProfileId, PostId
                                        FROM Comment
                                        WHERE id = @id;";
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    Comments comment = null;
                    while (reader.Read())
                    {
                        comment = new Comments()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            Subject = reader.GetString(reader.GetOrdinal("subject")),
                            Content = reader.GetString(reader.GetOrdinal("content")),
                            CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime")),
                            UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
                            Post = reader.GetInt32(reader.GetOrdinal("PostId")),
                        };

                    }
                    reader.Close();
                    return comment;

                }
            }
        }
        public void Update(Comments comments)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Update Comment set Subject = @subject, Content = @content
                                        where id = @id;";
                    cmd.Parameters.AddWithValue("@subject", comments.Subject);
                    cmd.Parameters.AddWithValue("@content", comments.Content);
                    cmd.Parameters.AddWithValue("@id", comments.Id);
                    cmd.ExecuteNonQuery();
                }
            }

        }

        public void DeleteComment(int commentId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            UPDATE Comment SET IsDeleted = 1
                            WHERE Id = @id
                        ";

                    cmd.Parameters.AddWithValue("@id", commentId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

    }//EOC
}
