using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using TabloidMVC.Models;


namespace TabloidMVC.Repositories
{
    public class TagRepository : BaseRepository,ITagRepository
    {
        public TagRepository(IConfiguration config) : base(config) { }

        public List<Tag> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT id, name FROM Tag Where IsDeleted = 0";
                    var reader = cmd.ExecuteReader();

                    var tags = new List<Tag>();

                    while (reader.Read())
                    {
                        tags.Add(new Tag()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("name")),
                        });
                    }

                    reader.Close();

                    return tags;
                }
            }
        }

        public List<Tag> GetAllByPost(int postId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT t.id, t.name FROM Tag t 
                                        Join PostTag pt on t.id = pt.TagId 
                                        Where pt.PostId = @postId And t.IsDeleted = 0";
                    cmd.Parameters.AddWithValue("@postId", postId);
                    var reader = cmd.ExecuteReader();

                    var tags = new List<Tag>();

                    while (reader.Read())
                    {
                        tags.Add(new Tag()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("name")),
                        });
                    }

                    reader.Close();

                    return tags;
                }
            }
        }

        public void DeletePostTag(int id, int postId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Delete From PostTag 
                                        where TagId = @id And PostId = @postId";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@postId", postId);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        public void AddPostTag(int id, int postId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "Select count(Id) From PostTag Where TagId=@id And PostId=@postId";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@postId", postId);
                    var tags = cmd.ExecuteScalar();
                    if(Convert.ToInt32(tags) == 0)
                    {
                        cmd.CommandText = @"Insert Into PostTag(TagId, PostId) 
                                        Values(@id, @postId)";
                        cmd.ExecuteNonQuery();
                    }
                    
                }
                conn.Close();
            }
        }


        public void Delete(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "Update Tag set IsDeleted = 1 where Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        public void Edit(Tag toEdit)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "Update Tag set Name = @name where Id = @id";
                    cmd.Parameters.AddWithValue("@name", toEdit.Name);
                    cmd.Parameters.AddWithValue("@id", toEdit.Id);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }


        public Tag GetTagById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT id, name FROM Tag Where IsDeleted = 0 And Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        return new Tag()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("name")),
                        };
                    }

                    reader.Close();
                }
                return null;
            }
        }
        public int Add(Tag add)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using(var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "Insert Into Tag(Name) OUTPUT INSERTED.ID Values(@name)";
                    cmd.Parameters.AddWithValue("@name", add.Name);
                    add.Id = (int)cmd.ExecuteScalar();
                }
                conn.Close();
                return add.Id;
            }
        }
    }
}
