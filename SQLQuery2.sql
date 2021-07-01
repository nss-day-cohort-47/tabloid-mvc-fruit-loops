SELECT  p.Content
FROM Subscription s  JOIN UserProfile up ON s.ProviderUserProfileId = up.Id 
 JOIN Post p ON p.UserProfileId = up.Id  
WHERE s.SubscriberUserProfileId = 1