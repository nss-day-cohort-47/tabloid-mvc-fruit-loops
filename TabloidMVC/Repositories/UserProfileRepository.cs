using Microsoft.Extensions.Configuration;

using System;

using System.Collections.Generic;

using TabloidMVC.Models;
using TabloidMVC.Utils;

namespace TabloidMVC.Repositories
{
    public class UserProfileRepository : BaseRepository, IUserProfileRepository
    {
        public UserProfileRepository(IConfiguration config) : base(config) { }

        public UserProfile GetByEmail(string email)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       SELECT u.id, u.FirstName, u.LastName, u.DisplayName, u.Email,
                              u.CreateDateTime, u.ImageLocation, u.UserTypeId,
                              ut.[Name] AS UserTypeName
                         FROM UserProfile u
                              LEFT JOIN UserType ut ON u.UserTypeId = ut.id
                        WHERE email = @email";
                    cmd.Parameters.AddWithValue("@email", email);

                    UserProfile userProfile = null;
                    var reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        userProfile = new UserProfile()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            DisplayName = reader.GetString(reader.GetOrdinal("DisplayName")),
                            CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime")),
                            //ImageLocation = DbUtils.GetNullableString(reader, "ImageLocation"),
                            UserTypeId = reader.GetInt32(reader.GetOrdinal("UserTypeId")),
                            UserType = new UserType()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("UserTypeId")),
                                Name = reader.GetString(reader.GetOrdinal("UserTypeName"))
                            },
                        };

                        //Check if optional columns are null
                        if (reader.IsDBNull(reader.GetOrdinal("ImageLocation")) == false)
                        {
                            userProfile.ImageLocation = reader.GetString(reader.GetOrdinal("ImageLocation"));
                        }

                    }

                    reader.Close();

                    return userProfile;
                }
            }
        }

        public UserProfile GetById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       SELECT u.id, u.FirstName, u.LastName, u.DisplayName, u.Email,
                              u.CreateDateTime, u.ImageLocation, u.UserTypeId,
                              ut.[Name] AS UserTypeName
                         FROM UserProfile u
                              LEFT JOIN UserType ut ON u.UserTypeId = ut.id
                        WHERE u.id = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    UserProfile userProfile = null;
                    var reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        userProfile = new UserProfile()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            DisplayName = reader.GetString(reader.GetOrdinal("DisplayName")),
                            CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime")),
                            //ImageLocation = DbUtils.GetNullableString(reader, "ImageLocation"),
                            UserTypeId = reader.GetInt32(reader.GetOrdinal("UserTypeId")),
                            UserType = new UserType()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("UserTypeId")),
                                Name = reader.GetString(reader.GetOrdinal("UserTypeName"))
                            },
                        };

                        //Check if optional columns are null
                        if (reader.IsDBNull(reader.GetOrdinal("ImageLocation")) == false)
                        {
                            userProfile.ImageLocation = reader.GetString(reader.GetOrdinal("ImageLocation"));
                        }

                    }

                    reader.Close();

                    return userProfile;
                }
            }
        }

        public UserProfile Add(UserProfile user)

   
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"

                        INSERT INTO Userprofile (
                            FirstName, LastName, DisplayName, Email, CreateDateTime,
                            UserTypeId )
                        OUTPUT INSERTED.ID
                        VALUES (
                            @FirstName, @LastName, @DisplayName, @Email, @CreateDateTime,
                            @UserTypeId)";
                    
                    cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", user.LastName);
            
                    cmd.Parameters.AddWithValue("@DisplayName", user.DisplayName );
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@UserTypeId", 2);
                    cmd.Parameters.AddWithValue("@CreateDateTime", DateTime.Now);
                    

                    user.Id = (int)cmd.ExecuteScalar();
                    return user;
                }
            }
        }
        
        public List<UserProfile> GetAllUserProfiles()
       
         {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"

                       SELECT up.FirstName, up.LastName, up.DisplayName, ut.Name, up.Id
                         FROM UserProfile up 
                        LEFT JOIN UserType ut ON up.UserTypeId = ut.Id
                        WHERE up.IsDeleted = 0
                        ORDER BY up.DisplayName
                            ";
                    var reader = cmd.ExecuteReader();

                    var userProfiles = new List<UserProfile>();

                    while (reader.Read())
                    {
                        userProfiles.Add(new UserProfile()
                        {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            DisplayName = reader.GetString(reader.GetOrdinal("DisplayName")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                           UserType  = new UserType()
                            {
                                Name = reader.GetString(reader.GetOrdinal("Name"))
                            }
                    });

                       
                    }

                    reader.Close();

                    return userProfiles;
                }
            }
        }



    }
}
