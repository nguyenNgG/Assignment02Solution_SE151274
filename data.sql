USE [eBookStoreDB]
GO
INSERT [dbo].[Authors] ([author_id], [last_name], [first_name], [phone], [address], [city], [state], [zip], [email_address]) VALUES (0, N'Belmont', N'Trevor', N'147614761476', N'Wallachia', N'Romania', N'Dracula''s Curse', N'1456', N'trevor@vamphunt.com')
GO
INSERT [dbo].[Authors] ([author_id], [last_name], [first_name], [phone], [address], [city], [state], [zip], [email_address]) VALUES (1, N'Belmont', N'Simon', N'169116911691', N'Wallachia', N'Romania', N'Simon''s Quest', N'1669', N'simon@vamphunt.com')
GO
INSERT [dbo].[Authors] ([author_id], [last_name], [first_name], [phone], [address], [city], [state], [zip], [email_address]) VALUES (2, N'Belmont', N'Richter', N'179217921792', N'Aljiba', N'Yuba Lake', N'Rondo of Blood', N'1773', N'richter@vamphunt.com')
GO
INSERT [dbo].[Authors] ([author_id], [last_name], [first_name], [phone], [address], [city], [state], [zip], [email_address]) VALUES (3, N'Morris', N'Jonathan', N'194419441944', N'Texas', N'USA', N'Portrait of Ruin', N'1926', N'jonathan@morris.org')
GO
INSERT [dbo].[Publishers] ([publisher_id], [publisher_name], [city], [state], [country]) VALUES (0, N'Belnades Publishings', N'Wallachia', N'Dracula''s Curse', N'Castlevania')
GO
INSERT [dbo].[Publishers] ([publisher_id], [publisher_name], [city], [state], [country]) VALUES (1, N'Renard Publishings', N'Yuba Lake', N'Rondo of Blood', N'Castlevania')
GO
INSERT [dbo].[Publishers] ([publisher_id], [publisher_name], [city], [state], [country]) VALUES (2, N'Morris Publishings', N'USA', N'Bloodlines', N'Castlevania')
GO
INSERT [dbo].[Books] ([book_id], [title], [type], [publisher_id], [price], [advance], [royalty], [ytd_sales], [notes], [published_date]) VALUES (0, N'Whips 101', N'Educational', 0, 1479.0000, 1479.0000, 1479.0000, 1479.0000, N'Vampire Killer', CAST(N'1989-12-22T00:00:00.0000000' AS DateTime2))
GO
INSERT [dbo].[Books] ([book_id], [title], [type], [publisher_id], [price], [advance], [royalty], [ytd_sales], [notes], [published_date]) VALUES (1, N'Symphony of the Night', N'Novel', 1, 1797.0000, 1797.0000, 1797.0000, 1797.0000, N'Alucard', CAST(N'1997-03-20T21:00:00.0000000' AS DateTime2))
GO
INSERT [dbo].[Books] ([book_id], [title], [type], [publisher_id], [price], [advance], [royalty], [ytd_sales], [notes], [published_date]) VALUES (2, N'Simon''s Quest', N'Autobiography', 0, 1698.0000, 1698.0000, 1698.0000, 1698.0000, N'The curse of the evil Count', CAST(N'1987-08-28T03:00:00.0000000' AS DateTime2))
GO
INSERT [dbo].[BookAuthors] ([author_id], [book_id], [author_order], [royalty_percentage]) VALUES (0, 0, 1, CAST(55.00 AS Decimal(5, 2)))
GO
INSERT [dbo].[BookAuthors] ([author_id], [book_id], [author_order], [royalty_percentage]) VALUES (1, 1, 2, CAST(45.00 AS Decimal(5, 2)))
GO
INSERT [dbo].[BookAuthors] ([author_id], [book_id], [author_order], [royalty_percentage]) VALUES (1, 2, 12, CAST(78.00 AS Decimal(5, 2)))
GO
INSERT [dbo].[BookAuthors] ([author_id], [book_id], [author_order], [royalty_percentage]) VALUES (2, 1, 2, CAST(33.00 AS Decimal(5, 2)))
GO
INSERT [dbo].[Roles] ([role_id], [role_desc]) VALUES (1, N'Reader')
GO
INSERT [dbo].[Roles] ([role_id], [role_desc]) VALUES (2, N'Publisher')
GO
INSERT [dbo].[Users] ([user_id], [email_address], [password], [source], [first_name], [middle_name], [last_name], [role_id], [publisher_id], [hire_date]) VALUES (1, N'member1@ebookstore.com', N'123', N'Clan', N'Quincy', N'Invisi', N'Morris', 1, 2, CAST(N'2020-01-20T00:32:00.0000000' AS DateTime2))
GO