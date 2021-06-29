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
                            Conent = reader.GetString(reader.GetOrdinal("content")),
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
                    cmd.CommandText = @"SELECT id, subject, content, CreateDateTime, UserProfileId, PostId
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
                            Conent = reader.GetString(reader.GetOrdinal("content")),
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

        public Comments GetCommentById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
