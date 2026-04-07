print '' print '' print 'Creating Stored Procedures in tcg_db'
GO
USE [tcg_db]
GO

/*========== Start Ability Stored Procedures ==========*/
print'' print'========== Start Ability Stored Procedures =========='

print '*** creating sp_select_ability_by_ability_id ***'
GO
CREATE PROCEDURE [dbo].[sp_select_ability_by_ability_id]
	(
		@AbilityID		[nvarchar](30)
	)
AS
	BEGIN
		SELECT 	[Ability].[AbilityID],[Ability].[AbilityType],
				[Ability].[Description],[Ability].[Active]
		FROM	[Ability]
		WHERE	[Ability].[AbilityID] = @AbilityID;
	END
GO

print '*** creating sp_select_all_abilities ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_abilities]
AS
	BEGIN
		SELECT 	[Ability].[AbilityID],[Ability].[AbilityType],
				[Ability].[Description],[Ability].[Active]
		FROM	[Ability];
	END
GO

print '*** creating sp_select_abilities_active_paginated ***'
GO
CREATE PROCEDURE [dbo].[sp_select_abilities_active_paginated]
(
	@PageNumber			[int] = 1,
	@PageSize			[int] = 20
)
AS
	BEGIN
		SELECT 	[Ability].[AbilityID],[Ability].[AbilityType],
				[Ability].[Description],[Ability].[Active],
				
				/*PaginatedList Components*/
				COUNT([AbilityID]) OVER() AS TotalCount,
				@PageNumber AS PageNumber, 
				@PageSize AS PageSize,
				CEILING(1.0 *  COUNT([AbilityID]) OVER() / @PageSize) AS TotalPages
		
		FROM	[Ability]
		WHERE	[Ability].[Active] = 1
		
		/*Pagination*/
		ORDER BY [AbilityID] DESC
		OFFSET	@PageSize * (@PageNumber - 1) ROWS
		FETCH NEXT @PageSize ROWS ONLY;
	END
GO

print '*** creating sp_select_abilities_deactive_paginated ***'
GO
CREATE PROCEDURE [dbo].[sp_select_abilities_deactive_paginated]
(
	@PageNumber			[int] = 1,
	@PageSize			[int] = 20
)
AS
	BEGIN
		SELECT 	[Ability].[AbilityID],[Ability].[AbilityType],
				[Ability].[Description],[Ability].[Active],
				
				/*PaginatedList Components*/
				COUNT([AbilityID]) OVER() AS TotalCount,
				@PageNumber AS PageNumber, 
				@PageSize AS PageSize,
				CEILING(1.0 *  COUNT([AbilityID]) OVER() / @PageSize) AS TotalPages
				
		FROM	[Ability]
		WHERE	[Ability].[Active] = 0
		
		/*Pagination*/
		ORDER BY [AbilityID] DESC
		OFFSET	@PageSize * (@PageNumber - 1) ROWS
		FETCH NEXT @PageSize ROWS ONLY;
	END
GO

print '*** creating sp_select_abilities_by_ability_type_paginated ***'
GO
CREATE PROCEDURE [dbo].[sp_select_abilities_by_ability_type_paginated]
	(
		@AbilityType	[nvarchar](25),
		@PageNumber			[int] = 1,
		@PageSize			[int] = 20

	)
AS
	BEGIN
		SELECT 	[Ability].[AbilityID],[Ability].[AbilityType],
				[Ability].[Description],[Ability].[Active],
				
				/*PaginatedList Components*/
				COUNT([AbilityID]) OVER() AS TotalCount,
				@PageNumber AS PageNumber, 
				@PageSize AS PageSize,
				CEILING(1.0 *  COUNT([AbilityID]) OVER() / @PageSize) AS TotalPages
				
		FROM	[Ability]
		WHERE	[Ability].[AbilityType] = @AbilityType
		AND		[Ability].[Active] = 1
		
		/*Pagination*/
		ORDER BY [AbilityID] DESC
		OFFSET	@PageSize * (@PageNumber - 1) ROWS
		FETCH NEXT @PageSize ROWS ONLY;
	END
GO

print '*** creating sp_insert_ability ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_ability]
	(
		@AbilityID			[nvarchar](30),
		@AbilityType		[nvarchar](25),
		@Description		[nvarchar](650)
	)	
AS
	BEGIN
		INSERT INTO [dbo].[Ability]
			([AbilityID],[AbilityType],[Description])
		VALUES
			(@AbilityID,@AbilityType,@Description)
		RETURN @@ROWCOUNT;
	END
GO

print '*** creating sp_update_ability ***'
GO
CREATE PROCEDURE [dbo].[sp_update_ability]
	(
		@AbilityID			[nvarchar](30),
		@AbilityType		[nvarchar](25),
		@Description		[nvarchar](650)
	)
AS
	BEGIN
		UPDATE 	[Ability]
		SET		[Ability].[AbilityType] = @AbilityType,
				[Ability].[Description] = @Description
		WHERE	[Ability].[AbilityID] = @AbilityID
		RETURN	@@ROWCOUNT;
	END
GO

print '*** creating sp_delete_ability ***'
GO
CREATE PROCEDURE [dbo].[sp_delete_ability]
	(
		@AbilityID		[nvarchar](30)
	)
AS
	BEGIN
		DELETE 	[Ability]
		WHERE 	[Ability].[AbilityID] = @AbilityID
		RETURN 	@@ROWCOUNT;
	END
GO

print '*** creating sp_deactivate_ability ***'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_ability]
	(
		@AbilityID		[nvarchar](30)
	)
AS
	BEGIN
		UPDATE 	[Ability]
		SET		[Active] = 0
		WHERE 	[Ability].[AbilityID] = @AbilityID
		RETURN 	@@ROWCOUNT;
	END
GO

print '*** creating sp_reactivate_ability ***'
GO
CREATE PROCEDURE [dbo].[sp_reactivate_ability]
	(
		@AbilityID		[nvarchar](30)
	)
AS
	BEGIN
		UPDATE 	[Ability]
		SET		[Active] = 1
		WHERE 	[Ability].[AbilityID] = @AbilityID
		RETURN 	@@ROWCOUNT;
	END
GO

print'========== End Ability Stored Procedures =========='
/*========== End Ability Stored Procedures ==========*/



/*========== Start AlternateArt Stored Procedures ==========*/
print'' print'========== Start AlternateArt Stored Procedures =========='

print '*** creating sp_select_alternate_art_by_alternate_art_id ***'
GO
CREATE PROCEDURE [dbo].[sp_select_alternate_art_by_alternate_art_id]
	(
		@AlternateArtID		[nvarchar](50)
	)
AS
	BEGIN
		SELECT 	[AlternateArtID],[Description],[Active]
		FROM	[AlternateArt]
		WHERE	[AlternateArt].[AlternateArtID] = @AlternateArtID;
	END
GO

print '*** creating sp_select_all_alternate_arts ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_alternate_arts]
(
		@PageNumber			[int] = 1,
		@PageSize			[int] = 20
)
AS
	BEGIN
		SELECT 	[AlternateArtID],[Description],[Active]	
		FROM	[AlternateArt];
	END
GO

print '*** creating sp_select_alternate_arts_active_paginated ***'
GO
CREATE PROCEDURE [dbo].[sp_select_alternate_arts_active_paginated]
(
		@PageNumber			[int] = 1,
		@PageSize			[int] = 20
)
AS
	BEGIN
		SELECT 	[AlternateArtID],[Description],[Active],
				
				/*PaginatedList Components*/
				COUNT([AlternateArtID]) OVER() AS TotalCount,
				@PageNumber AS PageNumber, 
				@PageSize AS PageSize,
				CEILING(1.0 *  COUNT([AlternateArtID]) OVER() / @PageSize) AS TotalPages
				
		FROM	[AlternateArt]
		WHERE	[AlternateArt].[Active] = 1
		
		/*Pagination*/
		ORDER BY [AlternateArtID] DESC
		OFFSET	@PageSize * (@PageNumber - 1) ROWS
		FETCH NEXT @PageSize ROWS ONLY;
	END
GO

print '*** creating sp_select_alternate_arts_deactive_paginated ***'
GO
CREATE PROCEDURE [dbo].[sp_select_alternate_arts_deactive_paginated]
(
		@PageNumber			[int] = 1,
		@PageSize			[int] = 20
)
AS
	BEGIN
		SELECT 	[AlternateArtID],[Description],[Active],
				
				/*PaginatedList Components*/
				COUNT([AlternateArtID]) OVER() AS TotalCount,
				@PageNumber AS PageNumber, 
				@PageSize AS PageSize,
				CEILING(1.0 *  COUNT([AlternateArtID]) OVER() / @PageSize) AS TotalPages
				
		FROM	[AlternateArt]
		WHERE	[AlternateArt].[Active] = 0
		
		/*Pagination*/
		ORDER BY [AlternateArtID] DESC
		OFFSET	@PageSize * (@PageNumber - 1) ROWS
		FETCH NEXT @PageSize ROWS ONLY;
	END
GO

print '*** creating sp_insert_alternate_art ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_alternate_art]
	(
		@AlternateArtID		[nvarchar](50),
		@Description		[nvarchar](250)
	)	
AS
	BEGIN
		INSERT INTO [dbo].[AlternateArt]
			([AlternateArtID],[Description])
		VALUES
			(@AlternateArtID,@Description)
		RETURN @@ROWCOUNT;
	END
GO

print '*** creating sp_update_alternate_art ***'
GO
CREATE PROCEDURE [dbo].[sp_update_alternate_art]
	(
		@AlternateArtID		[nvarchar](50),
		@Description		[nvarchar](250)
	)
AS
	BEGIN
		UPDATE 	[AlternateArt]
		SET		[Description] = @Description
		WHERE	[AlternateArtID] = @AlternateArtID
		RETURN	@@ROWCOUNT;
	END
GO

print '*** creating sp_delete_alternate_art ***'
GO
CREATE PROCEDURE [dbo].[sp_delete_alternate_art]
	(
		@AlternateArtID		[nvarchar](50)
	)
AS
	BEGIN
		DELETE 	[AlternateArt]
		WHERE 	[AlternateArtID] = @AlternateArtID
		RETURN 	@@ROWCOUNT;
	END
GO

print '*** creating sp_deactivate_alternate_art ***'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_alternate_art]
	(
		@AlternateArtID		[nvarchar](50)
	)
AS
	BEGIN
		UPDATE 	[AlternateArt]
		SET		[Active] = 0
		WHERE 	[AlternateArtID] = @AlternateArtID
		RETURN 	@@ROWCOUNT;
	END
GO

print '*** creating sp_reactivate_alternate_art ***'
GO
CREATE PROCEDURE [dbo].[sp_reactivate_alternate_art]
	(
		@AlternateArtID		[nvarchar](50)
	)
AS
	BEGIN
		UPDATE 	[AlternateArt]
		SET		[Active] = 1
		WHERE 	[AlternateArtID] = @AlternateArtID
		RETURN 	@@ROWCOUNT;
	END
GO

print'========== End AlternateArt Stored Procedures =========='
/*========== End AlternateArt Stored Procedures ==========*/



/*========== Start Artist Stored Procedures ==========*/
print'' print'========== Start Artist Stored Procedures =========='

print '*** creating sp_select_artist_by_artistid ***'
GO
CREATE PROCEDURE [dbo].[sp_select_artist_by_artistid]
	(
		@ArtistID		[int]
	)
AS
	BEGIN
		SELECT 	[ArtistID],[GivenName],[Surname],[Active]
		FROM	[Artist]
		WHERE	[Artist].[ArtistID] = @ArtistID;
	END
GO

print '*** creating sp_select_artist_by_given_name ***'
GO
CREATE PROCEDURE [dbo].[sp_select_artist_by_name]
	(
		@GivenName		[nvarchar](50),
		@Surname		[nvarchar](100)
	)
AS
	BEGIN
		SELECT 	[ArtistID],[GivenName],[Surname],[Active]
		FROM	[Artist]
		WHERE	[Artist].[GivenName] = @GivenName
		AND		[Artist].[Surname] = @Surname;
	END
GO

print '*** creating sp_select_all_artists ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_artists]
AS
	BEGIN
		SELECT 	[ArtistID],[GivenName],[Surname],[Active]
		FROM	[Artist];
	END
GO

print '*** creating sp_select_artists_active_paginated ***'
GO
CREATE PROCEDURE [dbo].[sp_select_artists_active_paginated]
(
		@PageNumber			[int] = 1,
		@PageSize			[int] = 20
)
AS
	BEGIN
		SELECT 	[ArtistID],[GivenName],[Surname],[Active],
				
				/*PaginatedList Components*/
				COUNT([ArtistID]) OVER() AS TotalCount,
				@PageNumber AS PageNumber, 
				@PageSize AS PageSize,
				CEILING(1.0 *  COUNT([ArtistID]) OVER() / @PageSize) AS TotalPages
				
		FROM	[Artist]
		WHERE	[Artist].[Active] = 1
		
		/*Pagination*/
		ORDER BY [GivenName] ASC
		OFFSET	@PageSize * (@PageNumber - 1) ROWS
		FETCH NEXT @PageSize ROWS ONLY;
	END
GO

print '*** creating sp_select_artists_deactive_paginated ***'
GO
CREATE PROCEDURE [dbo].[sp_select_artists_deactive_paginated]
(
		@PageNumber			[int] = 1,
		@PageSize			[int] = 20
)
AS
	BEGIN
		SELECT 	[ArtistID],[GivenName],[Surname],[Active],
				
				/*PaginatedList Components*/
				COUNT([ArtistID]) OVER() AS TotalCount,
				@PageNumber AS PageNumber, 
				@PageSize AS PageSize,
				CEILING(1.0 *  COUNT([ArtistID]) OVER() / @PageSize) AS TotalPages
				
		FROM	[Artist]
		WHERE	[Artist].[Active] = 0
		
		/*Pagination*/
		ORDER BY [GivenName] ASC
		OFFSET	@PageSize * (@PageNumber - 1) ROWS
		FETCH NEXT @PageSize ROWS ONLY;
	END
GO

print '*** creating sp_insert_artist ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_artist]
	(
		@GivenName		[nvarchar](50),
		@Surname		[nvarchar](100)
	)
AS
	BEGIN
		INSERT INTO [dbo].[Artist]
			([GivenName],[Surname])
		VALUES
			(@GivenName,@Surname)
		RETURN SCOPE_IDENTITY();
	END
GO

print '*** creating sp_update_artist ***'
GO
CREATE PROCEDURE [dbo].[sp_update_artist]
	(
		@ArtistID		[int],
		@GivenName		[nvarchar](50),
		@Surname		[nvarchar](100)
	)
AS
	BEGIN
		UPDATE 	[Artist]
		SET		[Artist].[GivenName] = @GivenName,
				[Artist].[Surname] = @Surname
		WHERE	[Artist].[ArtistID] = @ArtistID
		RETURN	@@ROWCOUNT;
	END
GO

print '*** creating sp_delete_artist ***'
GO
CREATE PROCEDURE [dbo].[sp_delete_artist]
	(
		@ArtistID		[int]
	)
AS
	BEGIN
		DELETE 	[Artist]
		WHERE 	[Artist].[ArtistID] = @ArtistID
		RETURN 	@@ROWCOUNT;
	END
GO

print '*** creating sp_deactivate_artist ***'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_artist]
	(
		@ArtistID		[int]
	)
AS
	BEGIN
		UPDATE 	[Artist]
		SET		[Active] = 0
		WHERE 	[ArtistID] = @ArtistID
		RETURN 	@@ROWCOUNT;
	END
GO

print '*** creating sp_reactivate_artist ***'
GO
CREATE PROCEDURE [dbo].[sp_reactivate_artist]
	(
		@ArtistID		[int]
	)
AS
	BEGIN
		UPDATE 	[Artist]
		SET		[Active] = 1
		WHERE 	[ArtistID] = @ArtistID
		RETURN 	@@ROWCOUNT;
	END
GO

print'========== End Artist Stored Procedures =========='
/*========== End Artist Stored Procedures ==========*/




print ''print '*** creating sp_authenticate_user_by_email_and_password_hash ***'
GO
CREATE PROCEDURE [dbo].[sp_authenticate_user_by_email_and_password_hash]
	(
		@Email				[nvarchar](250),
		@PasswordHash		[nvarchar](100)
	)
AS
	BEGIN
		SELECT	COUNT([Users].[UserID])
		FROM	[Users]
		WHERE	[Users].[Email] = @Email
			AND	[Users].[PasswordHash] = @PasswordHash
			AND	[Users].[Active] = 1;
	END
GO

print '*** creating sp_select_user_by_email ***'
GO
CREATE PROCEDURE [dbo].[sp_select_user_by_email]
	(
		@Email				[nvarchar](250)
	)
AS
	BEGIN
		SELECT	[Users].[UserID],[Users].[GivenName],[Users].[Surname],
					[Email],[Active]
		FROM	[Users]
		WHERE	[Email] = @Email;
	END
GO

print '*** creating sp_select_role_by_email ***'
GO
CREATE PROCEDURE [dbo].[sp_select_role_by_user_email]
	(
		@Email				[nvarchar](250)
	)
AS
	BEGIN
		SELECT	[UserRole].[RoleID]
		FROM	[UserRole] JOIN [Users] ON [UserRole].[UserID] = [Users].[UserID]
		WHERE	[Users].[Email] = @Email;
	END
GO

print '*** creating sp_insert_user_into_user ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_user_into_user]
	(
		@GivenName				[nvarchar](50),
		@Surname				[nvarchar](100),
		@Email					[nvarchar](250),
		@PasswordHash			[nvarchar](100)		
	)
AS
	BEGIN
		INSERT INTO [dbo].[Users]
			([GivenName],[Surname],[PasswordHash],[Email])
		VALUES
			(@GivenName,@Surname,@PasswordHash,@Email)
		RETURN SCOPE_IDENTITY();
	END
GO

print '*** creating sp_select_user_count_by_email ***'
GO
CREATE PROCEDURE [dbo].[sp_select_user_count_by_email]
	(
		@Email		[nvarchar](250)
	)
AS
	BEGIN
		SELECT	COUNT([Users].[UserID])
		FROM	[Users]
		WHERE	[Email] = @Email;
	END
GO	

print '*** creating sp_insert_user_into_role ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_user_into_role]
	(
		@UserID		[int],
		@RoleID		[nvarchar](50)			
	)
AS
	BEGIN
		INSERT INTO [dbo].[UserRole]
			([RoleID],[UserID])
		VALUES
			(@RoleID,@UserID)
		RETURN @@ROWCOUNT;
	END
GO

print '*** creating sp_update_passwordhash_by_email ***'
GO
CREATE PROCEDURE [dbo].[sp_update_passwordhash_by_email]
	(
		@Email					[nvarchar](250),
		@CurrentPasswordHash	[nvarchar](100),
		@NewPasswordHash		[nvarchar](100)
	)
AS
	BEGIN
		UPDATE 	[Users]
		SET		[Users].[PasswordHash] = @NewPasswordHash
		WHERE	[Users].[PasswordHash] = @CurrentPasswordHash
			AND	@CurrentPasswordHash != @NewPasswordHash
			AND	[Users].[Email] = @Email
		RETURN 	@@ROWCOUNT;
	END
GO

print '*** creating sp_select_element_by_element_type_id ***'
GO
CREATE PROCEDURE [dbo].[sp_select_element_by_element_type_id]
	(
		@ElementTypeID	[nvarchar](15)
	)
AS
	BEGIN
		SELECT 	[ElementType].[ElementTypeID],[ElementType].[Description]
		FROM	[ElementType]
		WHERE	[ElementType].[ElementTypeID] = @ElementTypeID;
	END
GO

print '*** creating sp_insert_element_type ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_element_type]
	(
		@ElementTypeID	[nvarchar](15),
		@Description	[nvarchar](100)
	)
AS
	BEGIN
		INSERT INTO [dbo].[ElementType]
			([ElementTypeID],[Description])
		VALUES
			(@ElementTypeID,@Description)
		RETURN @@ROWCOUNT;
	END
GO

print '*** creating sp_update_element_type ***'
GO
CREATE PROCEDURE [dbo].[sp_update_element_type]
	(
		@ElementTypeID	[nvarchar](15),
		@Description	[nvarchar](100)
	)
AS
	BEGIN
		UPDATE 	[ElementType]
		SET		[ElementType].[Description] = @Description
		WHERE	[ElementType].[ElementTypeID] = @ElementTypeID
		RETURN	@@ROWCOUNT;
	END
GO

print '*** creating sp_delete_element_type ***'
GO
CREATE PROCEDURE [dbo].[sp_delete_element_type]
	(
		@ElementTypeID	[nvarchar](15)
	)
AS
	BEGIN
		DELETE 	[ElementType]
		WHERE 	[ElementType].[ElementTypeID] = @ElementTypeID
		RETURN 	@@ROWCOUNT;
	END
GO

print '*** creating sp_select_elements ***'
GO
CREATE PROCEDURE [dbo].[sp_select_elements]
AS
	BEGIN
		SELECT 	[ElementType].[ElementTypeID],[ElementType].[Description]
		FROM	[ElementType];
	END
GO



print '*** creating sp_select_booster_by_boosterid ***'
GO
CREATE PROCEDURE [dbo].[sp_select_booster_by_boosterid]
	(
		@BoosterID		[nvarchar](50)
	)
AS
	BEGIN
		SELECT 	[Booster].[BoosterID],[Booster].[Series],
				[Booster].[ReleaseDate],[Booster].[Abbreviation]
		FROM	[Booster]
		WHERE	[Booster].[BoosterID] = @BoosterID;
	END
GO

print '*** creating sp_insert_booster ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_booster]
	(
		@BoosterID		[nvarchar](50),
		@Series			[nvarchar](50),
		@ReleaseDate	[date],
		@Abbreviation	[nvarchar](5)
	)	
AS
	BEGIN
		INSERT INTO [dbo].[Booster]
			([BoosterID],[Series],[ReleaseDate],[Abbreviation])
		VALUES
			(@BoosterID,@Series,@ReleaseDate,@Abbreviation)
		RETURN @@ROWCOUNT;
	END
GO

print '*** creating sp_update_booster ***'
GO
CREATE PROCEDURE [dbo].[sp_update_booster]
	(
		@BoosterID		[nvarchar](50),
		@Series			[nvarchar](50),
		@ReleaseDate	[date],
		@Abbreviation	[nvarchar](5)
	)
AS
	BEGIN
		UPDATE 	[Booster]
		SET		[Booster].[Series] = @Series,
				[Booster].[ReleaseDate] = @ReleaseDate,
				[Booster].[Abbreviation] = @Abbreviation
		WHERE	[Booster].[BoosterID] = @BoosterID
		RETURN	@@ROWCOUNT;
	END
GO

print '*** creating sp_delete_booster ***'
GO
CREATE PROCEDURE [dbo].[sp_delete_booster]
	(
		@BoosterID		[nvarchar](50)
	)
AS
	BEGIN
		DELETE 	[Booster]
		WHERE 	[Booster].[BoosterID] = @BoosterID
		RETURN 	@@ROWCOUNT;
	END
GO

print '*** creating sp_select_boosters ***'
GO
CREATE PROCEDURE [dbo].[sp_select_boosters]
AS
	BEGIN
		SELECT 	[Booster].[BoosterID],[Booster].[Series],
				[Booster].[ReleaseDate],[Booster].[Abbreviation]
		FROM	[Booster];
	END
GO







/*========== Start Pokemon Rule Stored Procedures ==========*/
print'' print'========== Start Pokemon Rule Stored Procedures =========='

print '*** creating sp_select_rule_by_rule_id ***'
GO
CREATE PROCEDURE [dbo].[sp_select_rule_by_rule_id]
	(
		@PokemonRuleID		[nvarchar](50)
	)
AS
	BEGIN
		SELECT 	[PokemonRuleID],[Description],[Active]
		FROM	[PokemonRule]
		WHERE	[PokemonRule].[PokemonRuleID] = @PokemonRuleID;
	END
GO

print '*** creating sp_select_rules ***'
GO
CREATE PROCEDURE [dbo].[sp_select_rules]
AS
	BEGIN
		SELECT 	[PokemonRuleID],[Description],[Active]
		FROM	[PokemonRule];
	END
GO

print '*** creating sp_select_rule_active_paginated ***'
GO
CREATE PROCEDURE [dbo].[sp_select_rule_active_paginated]
(
	@PageNumber			[int] = 1,
	@PageSize			[int] = 20
)
AS
	BEGIN
		SELECT 	[PokemonRuleID],[Description],[Active],
				
				/*PaginatedList Components*/
				COUNT([PokemonRuleID]) OVER() AS TotalCount,
				@PageNumber AS PageNumber, 
				@PageSize AS PageSize,
				CEILING(1.0 *  COUNT([PokemonRuleID]) OVER() / @PageSize) AS TotalPages
		
		FROM	[PokemonRule]
		WHERE	[Active] = 1
		
		/*Pagination*/
		ORDER BY [PokemonRuleID] DESC
		OFFSET	@PageSize * (@PageNumber - 1) ROWS
		FETCH NEXT @PageSize ROWS ONLY;
	END
GO

print '*** creating sp_select_rule_deactive_paginated ***'
GO
CREATE PROCEDURE [dbo].[sp_select_rule_deactive_paginated]
(
	@PageNumber			[int] = 1,
	@PageSize			[int] = 20
)
AS
	BEGIN
		SELECT 	[PokemonRuleID],[Description],[Active],
				
				/*PaginatedList Components*/
				COUNT([PokemonRuleID]) OVER() AS TotalCount,
				@PageNumber AS PageNumber, 
				@PageSize AS PageSize,
				CEILING(1.0 *  COUNT([PokemonRuleID]) OVER() / @PageSize) AS TotalPages
		
		FROM	[PokemonRule]
		WHERE	[Active] = 0
		
		/*Pagination*/
		ORDER BY [PokemonRuleID] DESC
		OFFSET	@PageSize * (@PageNumber - 1) ROWS
		FETCH NEXT @PageSize ROWS ONLY;
	END
GO

print '*** creating sp_insert_rule ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_rule]
	(
		@PokemonRuleID		[nvarchar](50),
		@Description		[nvarchar](150)
	)	
AS
	BEGIN
		INSERT INTO [dbo].[PokemonRule]
			([PokemonRuleID],[Description])
		VALUES
			(@PokemonRuleID,@Description)
		RETURN @@ROWCOUNT;
	END
GO

print '*** creating sp_update_rule ***'
GO
CREATE PROCEDURE [dbo].[sp_update_rule]
	(
		@PokemonRuleID		[nvarchar](50),
		@Description		[nvarchar](150)
	)
AS
	BEGIN
		UPDATE 	[PokemonRule]
		SET		[PokemonRule].[Description] = @Description
		WHERE	[PokemonRule].[PokemonRuleID] = @PokemonRuleID
		RETURN	@@ROWCOUNT;
	END
GO

print '*** creating sp_delete_rule ***'
GO
CREATE PROCEDURE [dbo].[sp_delete_rule]
	(
		@PokemonRuleID		[nvarchar](50)
	)
AS
	BEGIN
		DELETE 	[PokemonRule]
		WHERE 	[PokemonRule].[PokemonRuleID] = @PokemonRuleID
		RETURN 	@@ROWCOUNT;
	END
GO

print '*** creating sp_deactivate_rule ***'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_rule]
	(
		@PokemonRuleID		[nvarchar](50)
	)
AS
	BEGIN
		UPDATE 	[PokemonRule]
		SET		[Active] = 0
		WHERE 	[PokemonRuleID] = @PokemonRuleID
		RETURN 	@@ROWCOUNT;
	END
GO

print '*** creating sp_reactivate_rule ***'
GO
CREATE PROCEDURE [dbo].[sp_reactivate_rule]
	(
		@PokemonRuleID		[nvarchar](50)
	)
AS
	BEGIN
		UPDATE 	[PokemonRule]
		SET		[Active] = 1
		WHERE 	[PokemonRuleID] = @PokemonRuleID
		RETURN 	@@ROWCOUNT;
	END
GO

print'========== End Pokemon Rule Stored Procedures =========='
/*========== End Pokemon Rule Stored Procedures ==========*/



/*========== Start Move Stored Procedures ==========*/
print'' print'========== Start Move Stored Procedures =========='

print '*** creating sp_select_move_by_moveid ***'
GO
CREATE PROCEDURE [dbo].[sp_select_move_by_moveid]
	(
		@MoveID			[int]
	)
AS
	BEGIN
		SELECT 	[MoveID],[Name],[Damage],[Description],[Active]
		FROM	[Move]
		WHERE	[Move].[MoveID] = @MoveID;
	END
GO

print '*** creating sp_select_move_cost_by_moveid ***'
GO
CREATE PROCEDURE [dbo].[sp_select_move_cost_by_moveid]
	(
		@MoveID			[int]
	)
AS
	BEGIN
		SELECT	[MoveCost].[MoveID], [MoveCost].[ElementTypeID],
				[MoveCost].[Quantity]
		FROM	[MoveCost] JOIN [ElementType] ON 
				[MoveCost].[ElementTypeID] = [ElementType].[ElementTypeID]
		WHERE	[MoveCost].[MoveID] = @MoveID;
	END
GO

print '*** creating sp_select_moves_with_move_cost ***'
GO
CREATE PROCEDURE [dbo].[sp_select_moves_with_move_cost]
AS
	BEGIN
		SELECT 	[Move].[MoveID], [Move].[Name], [Move].[Damage],
				[Move].[Description], [Move].[Active],
				[MoveCost].[ElementTypeID],[MoveCost].[Quantity]
		FROM	[Move] JOIN [MoveCost] ON [Move].[MoveID] = [MoveCost].[MoveID];
	END
GO

print '*** creating sp_select_moves_without_move_cost ***'
GO
CREATE PROCEDURE [dbo].[sp_select_moves_without_move_cost]
AS
	BEGIN
		SELECT 	[MoveID],[Name],[Damage],[Description],[Active]
		FROM 	[Move] 
		WHERE	NOT EXISTS
			(
				SELECT 	[MoveCost].[MoveID]
				FROM 	[MoveCost]
				WHERE 	[MoveCost].[MoveID] = [Move].[MoveID]
			);
	END
GO

print '*** creating sp_select_moves_active_paginated ***'
GO
CREATE PROCEDURE [dbo].[sp_select_moves_active_paginated]
(
		@PageNumber			[int] = 1,
		@PageSize			[int] = 20
)
AS
	BEGIN
		SELECT 	[MoveID],[Name],[Damage],[Description],[Active],
		
				/*PaginatedList Components*/
				COUNT([MoveID]) OVER() AS TotalCount,
				@PageNumber AS PageNumber, 
				@PageSize AS PageSize,
				CEILING(1.0 *  COUNT([MoveID]) OVER() / @PageSize) AS TotalPages
				
		FROM	[Move]
		WHERE	[Move].[Active] = 1
		
		/*Pagination*/
		ORDER BY [Name] ASC
		OFFSET	@PageSize * (@PageNumber - 1) ROWS
		FETCH NEXT @PageSize ROWS ONLY;
	END
GO
print '*** creating sp_select_moves_deactive_paginated ***'
GO
CREATE PROCEDURE [dbo].[sp_select_moves_deactive_paginated]
(
		@PageNumber			[int] = 1,
		@PageSize			[int] = 20
)
AS
	BEGIN
		SELECT 	[MoveID],[Name],[Damage],[Description],[Active],
		
				/*PaginatedList Components*/
				COUNT([MoveID]) OVER() AS TotalCount,
				@PageNumber AS PageNumber, 
				@PageSize AS PageSize,
				CEILING(1.0 *  COUNT([MoveID]) OVER() / @PageSize) AS TotalPages
				
		FROM	[Move]
		WHERE	[Move].[Active] = 0
		
		/*Pagination*/
		ORDER BY [Name] ASC
		OFFSET	@PageSize * (@PageNumber - 1) ROWS
		FETCH NEXT @PageSize ROWS ONLY;
	END
GO

print '*** creating sp_insert_move ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_move]
	(	
		@Name			[nvarchar](30),
		@Damage			[int],
		@Description	[nvarchar](200)
	)	
AS
	BEGIN
		INSERT INTO [dbo].[Move]
			([Name],[Damage],[Description])
		VALUES
			(@Name,@Damage,@Description);
			
		SELECT SCOPE_IDENTITY();
	END
GO

print '*** creating sp_insert_move_cost ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_move_cost]
	(	
		@MoveID			[int],
		@ElementTypeID	[nvarchar](15),
		@Quantity		[int]
	)	
AS
	BEGIN
		INSERT INTO [dbo].[MoveCost]
			([MoveID],[ElementTypeID],[Quantity])
		VALUES
			(@MoveID,@ElementTypeID,@Quantity)
		RETURN @@ROWCOUNT;
	END
GO

print '*** creating sp_update_move ***'
GO
CREATE PROCEDURE [dbo].[sp_update_move]
	(	
		@MoveID			[int],
		@Name			[nvarchar](30),
		@Damage			[int],
		@Description	[nvarchar](200)
	)	
AS
	BEGIN
		UPDATE	[dbo].[Move]
		SET		[Move].[Name] = @Name,
				[Move].[Damage] = @Damage,
				[Move].[Description] = @Description
		WHERE	[Move].[MoveID] = @MoveID;
	END
GO

print '*** creating sp_delete_move ***'
GO
CREATE PROCEDURE [dbo].[sp_delete_move]
	(	
		@MoveID			[int]
	)	
AS
	BEGIN
		DELETE 	[dbo].[Move]
		WHERE	[Move].[MoveID] = @MoveID
		RETURN @@ROWCOUNT;
	END
GO

print '*** creating sp_delete_move_cost ***'
GO
CREATE PROCEDURE [dbo].[sp_delete_move_cost]
	(	
		@MoveID			[int]
	)	
AS
	BEGIN
		DELETE 	[dbo].[MoveCost]
		WHERE	[MoveCost].[MoveID] = @MoveID
		RETURN 	@@ROWCOUNT;
	END
GO

print '*** creating sp_deactivate_move ***'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_move]
	(
		@MoveID			[int]
	)
AS
	BEGIN
		UPDATE 	[Move]
		SET		[Active] = 0
		WHERE 	[MoveID] = @MoveID
		RETURN 	@@ROWCOUNT;
	END
GO

print '*** creating sp_reactivate_move ***'
GO
CREATE PROCEDURE [dbo].[sp_reactivate_move]
	(
		@MoveID		[int]
	)
AS
	BEGIN
		UPDATE 	[Move]
		SET		[Active] = 1
		WHERE 	[MoveID] = @MoveID
		RETURN 	@@ROWCOUNT;
	END
GO

print'========== End Move Stored Procedures =========='
/*========== End Move Stored Procedures ==========*/

print''


print '*** creating sp_select_card_by_card_id ***'
GO
CREATE PROCEDURE [dbo].[sp_select_card_by_card_id]
	(
		@PokemonCardID	[int]
	)
AS
	BEGIN
		SELECT	[PokemonCardID],[ArtistID],[AbilityID],			
				[BoosterID],[PokemonRuleID],[ElementTypeID],
				[Name],[BoosterNumber],[CardType],
				[Rarity],[WeaknessType],[ResistanceType],
				[WeaknessValue],[ResistanceValue],[RetreatCost],
				[Health],[Stage],[ImagePath]	
				
		FROM	[PokemonCard]
		WHERE	[PokemonCardID] = @PokemonCardID;
	END
GO

print '*** creating sp_select_moves_by_card_id ***'
GO
CREATE PROCEDURE [dbo].[sp_select_moves_by_card_id]
	(
		@PokemonCardID	[int]
	)
AS
	BEGIN
		SELECT 	[Move].[MoveID], [Move].[Name], [Move].[Damage], [Move].[Description],
				[MoveCost].[ElementTypeID],[MoveCost].[Quantity]
		FROM	[Move] LEFT JOIN [MoveCost] ON [Move].[MoveID] = [MoveCost].[MoveID]
			JOIN [CardMove] ON [Move].[MoveID] = [CardMove].[MoveID]
		WHERE	[CardMove].[PokemonCardID] = @PokemonCardID;
	END
GO

print '*** creating sp_select_alternate_arts_by_card_id ***'
GO
CREATE PROCEDURE [dbo].[sp_select_alternate_arts_by_card_id]
	(
		@PokemonCardID	[int]
	)
AS
	BEGIN
		SELECT 	[AlternateArtID]
		FROM	[CardAlternateArt]
		WHERE	[PokemonCardID] = @PokemonCardID;
	END
GO

print '*** creating sp_select_all_cards ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_cards]
AS
	BEGIN
		SELECT	[PokemonCardID],[BoosterID],[ElementTypeID],
				[Name],[BoosterNumber],[CardType],[Rarity],[ImagePath]
				
		FROM	[PokemonCard];
	END
GO
/*
print '*** creating sp_select_card_moves ***'
GO
CREATE PROCEDURE [dbo].[sp_select_card_moves]
AS
	BEGIN
		SELECT 	[CardMove].[PokemonCardID],[Move].[MoveID], [Move].[Name], [Move].[Damage], [Move].[Description],
				[MoveCost].[ElementTypeID],[MoveCost].[Quantity]
		FROM	[Move] LEFT JOIN [MoveCost] ON [Move].[MoveID] = [MoveCost].[MoveID]
			JOIN [CardMove] ON [Move].[MoveID] = [CardMove].[MoveID];
	END
GO

print '*** creating sp_select_card_alternate_arts ***'
GO
CREATE PROCEDURE [dbo].[sp_select_card_alternate_arts]
AS
	BEGIN
		SELECT 	[CardAlternateArt].[PokemonCardID], [CardAlternateArt].[AlternateArtID]
		FROM	[CardAlternateArt];
	END
GO
*/

print '*** creating sp_select_cards_by_card_name ***'
GO
CREATE PROCEDURE [dbo].[sp_select_cards_by_card_name]
	(
		@Name			[nvarchar](50)
	)
AS
	BEGIN
		SELECT	[PokemonCardID],[BoosterID],[ElementTypeID],
				[Name],[BoosterNumber],[CardType],[Rarity],[ImagePath]
				
		FROM	[PokemonCard]
		WHERE	[PokemonCard].[Name] LIKE CONCAT('%',@Name,'%');
	END
GO

/*
print '*** creating sp_select_card_moves_by_card_name ***'
GO
CREATE PROCEDURE [dbo].[sp_select_card_moves_by_card_name]
	(
		@Name			[nvarchar](50)
	)
AS
	BEGIN
		SELECT 	[PokemonCard].[PokemonCardID],[Move].[MoveID], [Move].[Name], [Move].[Damage], [Move].[Description],
				[MoveCost].[ElementTypeID],[MoveCost].[Quantity]
		FROM	[Move] LEFT JOIN [MoveCost] ON [Move].[MoveID] = [MoveCost].[MoveID]
			JOIN [CardMove] ON [Move].[MoveID] = [CardMove].[MoveID]
			JOIN [PokemonCard] ON [CardMove].[PokemonCardID] = [PokemonCard].[PokemonCardID]
		WHERE	[PokemonCard].[Name] LIKE CONCAT('%',@Name,'%');
	END
GO

print '*** creating sp_select_card_alternate_arts_by_card_name ***'
GO
CREATE PROCEDURE [dbo].[sp_select_card_alternate_arts_by_card_name]
	(
		@Name			[nvarchar](50)
	)
AS
	BEGIN
		SELECT 	[PokemonCard].[PokemonCardID], [CardAlternateArt].[AlternateArtID]
		FROM	[CardAlternateArt] JOIN [PokemonCard]
			ON [PokemonCard].[PokemonCardID] = [CardAlternateArt].[PokemonCardID]
		WHERE	[PokemonCard].[Name] LIKE CONCAT('%',@Name,'%');
	END
GO
*/
print '*** creating sp_delete_card ***'
GO
CREATE PROCEDURE [dbo].[sp_delete_card]
	(
		@PokemonCardID	[int]
	)
AS
	BEGIN
		DELETE 	[PokemonCard]
		WHERE	[PokemonCardID] = @PokemonCardID;
		RETURN @@ROWCOUNT;
	END
GO

print '*** creating sp_insert_default_user_collections ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_default_user_collections]
	(
		@UserID			[int]
	)
AS
	BEGIN
		INSERT INTO [dbo].[Collection]
	([UserID],[CollectionTypeID],[Name],[Description])
	VALUES
	(@UserID,'user','My Cards','A list of all my cards.'),
	(@UserID,'Wishlist','Wishlist','List of cards I want to get.'),
	(@UserID,'Favorites','Favorites','List of my favorite cards.')
	END
GO


print '*** creating sp_select_cards_by_collection_id ***'
GO
	CREATE PROCEDURE [dbo].[sp_select_collection_cards_by_collection_id]
	(
		@CollectionID			[int]
	)
AS
	BEGIN
		SELECT	[PokemonCard].[PokemonCardID],[PokemonCard].[ArtistID],[PokemonCard].[AbilityID],
				[PokemonCard].[BoosterID],[PokemonCard].[PokemonRuleID],[PokemonCard].[ElementTypeID],
				[PokemonCard].[Name],[PokemonCard].[BoosterNumber],[PokemonCard].[CardType],
				[PokemonCard].[Rarity],[PokemonCard].[WeaknessType],[PokemonCard].[ResistanceType],
				[PokemonCard].[WeaknessValue],[PokemonCard].[ResistanceValue],[PokemonCard].[RetreatCost],
				[PokemonCard].[Health],[PokemonCard].[Stage],[PokemonCard].[ImagePath],[CollectionCard].[CollectionCardID],
				[CollectionCard].[CollectionID],[CollectionCard].[Quantity],[CollectionCard].[Owned]
				
		FROM	[PokemonCard] 
			JOIN [CollectionCard] ON [CollectionCard].[PokemonCardID] = [PokemonCard].[PokemonCardID]
		
		WHERE	[CollectionCard].[CollectionID] = @CollectionID
			
	END
GO

print '*** creating sp_select_collection_by_user_id ***'
GO
	CREATE PROCEDURE [sp_select_collection_by_user_id]
	(
		@UserID					[int]
	)
AS
	BEGIN
		SELECT	[Collection].[CollectionID],[Collection].[UserID],[Collection].[CollectionTypeID],
				[Collection].[Name],[Collection].[Description]
		FROM	[Collection]
		WHERE	[Collection].[UserID] = @UserID;
	END
GO

print '*** creating sp_select_max_size_by_collection_type_id ***'
GO
	CREATE PROCEDURE [sp_select_max_size_by_collection_type_id]
	(
		@CollectionTypeID		[nvarchar](25)
	)
AS
	BEGIN
		SELECT	[CollectionType].[MaxSize]
		FROM	[CollectionType]
		WHERE	[CollectionType].[CollectionTypeID] = @CollectionTypeID;
	END
GO

print '*** creating sp_select_collection_elements_by_collection_id ***'
GO
	CREATE PROCEDURE [sp_select_collection_elements_by_collection_id]
	(
		@CollectionID		[int]
	)
AS
	BEGIN
		SELECT	[CollectionElement].[ElementTypeID]
		FROM	[CollectionElement]
		WHERE	[CollectionElement].[CollectionID] = @CollectionID;
	END
GO

print '*** creating sp_select_collection_by_collection_id ***'
GO
	CREATE PROCEDURE [sp_select_collection_by_collection_id]
	(
		@CollectionID		[int]
	)
AS
	BEGIN
		SELECT	[Collection].[CollectionID],[Collection].[UserID],[Collection].[CollectionTypeID],
				[Collection].[Name],[Collection].[Description]
		FROM	[Collection]
		WHERE	[Collection].[CollectionID] = @CollectionID;
	END
GO

print '*** creating sp_delete_collection_card ***'
GO
	CREATE PROCEDURE [sp_delete_collection_card]
	(
		@CollectionCardID	[int]
	)
AS
	BEGIN
		DELETE	[dbo].[CollectionCard]
		WHERE	[CollectionCard].[CollectionCardID] = @CollectionCardID
		RETURN 	@@ROWCOUNT;
	END
GO

print '*** creating sp_delete_collection ***'
GO
	CREATE PROCEDURE [sp_delete_collection]
	(
		@CollectionID	[int]
	)
AS
	BEGIN
		DELETE	[dbo].[Collection]
		WHERE	[Collection].[CollectionID] = @CollectionID
		RETURN 	@@ROWCOUNT;
	END
GO

print '*** creating sp_insert_collection_card ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_collection_card]
	(	
		@PokemonCardID		[int],
		@CollectionID		[int],
		@Quantity			[int],
		@Owned				[bit]
	)	
AS
	BEGIN
		INSERT INTO [dbo].[CollectionCard]
			([PokemonCardID],[CollectionID],[Quantity],[Owned])
		VALUES
			(@PokemonCardID,@CollectionID,@Quantity,@Owned)
		RETURN @@ROWCOUNT;
	END
GO

print '*** creating sp_insert_collection ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_collection]
	(	
		@UserID				[int],
		@CollectionTypeID	[nvarchar](25),
		@Name				[nvarchar](50),
		@Description		[nvarchar](150)
		
	)	
AS
	BEGIN
		INSERT INTO [dbo].[Collection]
			([UserID],[CollectionTypeID],[Name],[Description])
		VALUES
			(@UserID,@CollectionTypeID,@Name,@Description)
		RETURN @@ROWCOUNT;
	END
GO

print '*** creating sp_select_cards_by_collection_id ***'
GO
	CREATE PROCEDURE [dbo].[sp_select_newest_booster_card]
	(
		@ReleaseDate			[date]
	)
AS
	BEGIN
		SELECT	[PokemonCard].[PokemonCardID],[PokemonCard].[ArtistID],[PokemonCard].[AbilityID],
				[PokemonCard].[BoosterID],[PokemonCard].[PokemonRuleID],[PokemonCard].[ElementTypeID],
				[PokemonCard].[Name],[PokemonCard].[BoosterNumber],[PokemonCard].[CardType],
				[PokemonCard].[Rarity],[PokemonCard].[WeaknessType],[PokemonCard].[ResistanceType],
				[PokemonCard].[WeaknessValue],[PokemonCard].[ResistanceValue],[PokemonCard].[RetreatCost],
				[PokemonCard].[Health],[PokemonCard].[Stage],[PokemonCard].[ImagePath]
				
		FROM	[PokemonCard] 
			JOIN [Booster] ON [Booster].[BoosterID] = [PokemonCard].[BoosterID]
		
		WHERE	[Booster].[ReleaseDate] = @ReleaseDate
			
	END
GO

print '*** creating sp_insert_card_move ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_card_move]
	(	
		@PokemonCardID		[int],
		@MoveID				[int]
	)	
AS
	BEGIN
		INSERT INTO [dbo].[CardMove]
			([PokemonCardID],[MoveID])
		VALUES
			(@PokemonCardID,@MoveID)
		RETURN @@ROWCOUNT;
	END
GO

print '*** creating sp_insert_card_alternate_art ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_card_alternate_art]
	(	
		@PokemonCardID		[int],
		@AlternateArtID		[nvarchar](50)
		
	)	
AS
	BEGIN
		INSERT INTO [dbo].[CardAlternateArt]
			([PokemonCardID],[AlternateArtID])
		VALUES
			(@PokemonCardID,@AlternateArtID)
		RETURN @@ROWCOUNT;
	END
GO

print '*** creating sp_delete_card_moves ***'
GO
CREATE PROCEDURE [dbo].[sp_delete_card_moves]
	(	
		@PokemonCardID		[int]
	)	
AS
	BEGIN
		DELETE	[dbo].[CardMove]
		WHERE	[CardMove].[PokemonCardID] = @PokemonCardID
		RETURN 	@@ROWCOUNT;
	END
GO

print '*** creating sp_delete_card_alternate_arts ***'
GO
CREATE PROCEDURE [dbo].[sp_delete_card_alternate_arts]
	(	
		@PokemonCardID		[int]
		
	)	
AS
	BEGIN
		DELETE	[dbo].[CardAlternateArt]
		WHERE	[CardAlternateArt].[PokemonCardID] = @PokemonCardID
		RETURN 	@@ROWCOUNT;
	END
GO

print '*** creating sp_insert_card ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_card]
	(	
		@ArtistID				[int],		
		@AbilityID				[nvarchar](30),
		@BoosterID				[nvarchar](50),
		@PokemonRuleID			[nvarchar](50),
		@ElementTypeID			[nvarchar](15),
		@Name					[nvarchar](50),
		@BoosterNumber         	[int],		
		@CardType				[nvarchar](50),
		@Rarity					[nvarchar](30),
		@WeaknessType			[nvarchar](15),
		@ResistanceType        	[nvarchar](15),
		@WeaknessValue         	[int],         
		@ResistanceValue       	[int],         
		@RetreatCost           	[int],        
		@Health					[int],		
		@Stage					[nvarchar](30),
		@ImagePath				[nvarchar](250) = 'cards/default.png'
	)	
AS
	BEGIN
		INSERT INTO [dbo].[PokemonCard]
			([ArtistID],[AbilityID],[BoosterID],[PokemonRuleID],
			 [ElementTypeID],[Name],[BoosterNumber],[CardType],
			 [Rarity],[WeaknessType],[ResistanceType],[WeaknessValue],
			 [ResistanceValue],[RetreatCost],[Health],[Stage],[ImagePath])
		VALUES
			(@ArtistID,@AbilityID,@BoosterID,@PokemonRuleID,
			 @ElementTypeID,@Name,@BoosterNumber,@CardType,
			 @Rarity,@WeaknessType,@ResistanceType,@WeaknessValue,
			 @ResistanceValue,@RetreatCost,@Health,@Stage,@ImagePath);
			 
		SELECT SCOPE_IDENTITY();
	END
GO

print '*** creating sp_update_card ***'
GO
CREATE PROCEDURE [dbo].[sp_update_card]
	(	
		@PokemonCardID			[int],
		@ArtistID				[int],		
		@AbilityID				[nvarchar](30),
		@BoosterID				[nvarchar](50),
		@PokemonRuleID			[nvarchar](50),
		@ElementTypeID			[nvarchar](15),
		@Name					[nvarchar](50),
		@BoosterNumber         	[int],		
		@CardType				[nvarchar](50),
		@Rarity					[nvarchar](30),
		@WeaknessType			[nvarchar](15),
		@ResistanceType        	[nvarchar](15),
		@WeaknessValue         	[int],         
		@ResistanceValue       	[int],         
		@RetreatCost           	[int],        
		@Health					[int],		
		@Stage					[nvarchar](30),
		@ImagePath				[nvarchar](250) = 'cards/default.png'
	)	
AS
	BEGIN
		UPDATE 	[dbo].[PokemonCard]
		SET		[PokemonCard].[ArtistID] = @ArtistID,
				[PokemonCard].[AbilityID] = @AbilityID,
				[PokemonCard].[BoosterID] = @BoosterID,
				[PokemonCard].[PokemonRuleID] = @PokemonRuleID,
				[PokemonCard].[ElementTypeID] = @ElementTypeID,
				[PokemonCard].[Name] = @Name,
				[PokemonCard].[BoosterNumber] = @BoosterNumber,
				[PokemonCard].[CardType] = @CardType,
				[PokemonCard].[Rarity] = @Rarity,
				[PokemonCard].[WeaknessType] = @WeaknessType,
				[PokemonCard].[ResistanceType] = @ResistanceType,
				[PokemonCard].[WeaknessValue] = @WeaknessValue,
				[PokemonCard].[ResistanceValue] = @ResistanceValue,
				[PokemonCard].[RetreatCost] = @RetreatCost,
				[PokemonCard].[Health] = @Health,
				[PokemonCard].[Stage] = @Stage,
				[PokemonCard].[ImagePath] = @ImagePath		
		WHERE 	[PokemonCard].[PokemonCardID] = @PokemonCardID
		RETURN @@ROWCOUNT;
	END
GO