BEGIN
	IF NOT EXISTS (SELECT * FROM SurveyQuestions)
	BEGIN
	INSERT INTO [dbo].[SurveyQuestions] ([Id], [Text], [CreatedAtUtc], [QuestionNumber], [TypeId], [ActiveStatus], [Weight], [Category]) 
	VALUES
		--TypeId: Text = 1, Checkbox, Radio
		--Category: None = 0, Teach, Practice, Other, Default
		--ActiveStatus: Inactive = 0, Active, Archived


		--Teach
		('736f41a8-5f74-4be7-9956-c1250639f768', 'If you teach, where at?', '2019-10-29 17:58:39', 1, 1, 1, 0, 1),
		('ff8cea86-6915-4eef-8a1e-de976525b149', 'How long have you been teaching yoga?', '2019-11-26 06:40:20',  2, 3, 1, 0, 1),
		('931e7eb4-dc93-4b7b-8d9f-c030856cff03', 'Would you be interested in becoming a teacher for Yoga 4 Change?', '2019-04-09 11:52:41', 3, 3, 1, 1, 1),

		--Practice
		('93333504-8807-4ca1-b17b-9d4440d1a352', 'Have you practiced with us before?', '2019-11-02 01:56:30', 3, 3, 1, 0, 2),
		('4d7fc33f-0986-45a0-a92d-3c36b451c08c', 'If you practice, where at?', '2019-11-02 01:56:30', 4, 1, 1, 0, 2),
		('eef07c54-4329-48c4-88fa-fc7c7cf36da0', 'Would you be interested in practicing at a Yoga 4 Change facility?', '2019-02-10 21:49:07', 6, 3, 1, 1, 2),

		--Other
		('82fb6add-449d-4758-a97e-33731437928b', 'Would you like to volunteer?', '2019-04-11 21:58:48', 7, 3, 1, 1, 3),
		('ec52973d-a23d-470c-91a8-a7ba7086834c', 'Where did you hear about us?', '2019-11-22 01:46:52', 8, 1, 1, 0, 3),

		--None
		('e2f1641a-9bb2-4086-801a-23381382da7c', 'Do you like yoga?', '2019-11-22 01:46:52', 8, 2, 1, 0, 0),

		--Default
		('C4A198DD-D450-4DF5-A086-CE2AEDE6B643', 'What gender do you identify with?', '2019-11-22 01:46:52', -4, 3, 1, 0, 4),
		('54E8941B-4901-469B-950F-DD1D97620BDA', 'What ethnicity do you identify with?', '2019-11-22 01:46:52', -3, 3, 1, 0, 4),
		('DED673D6-960C-4C59-A906-02CB7253D0EA', 'What is the highest degree you have earned?', '2019-11-22 01:46:52', -2, 3, 1, 0, 4),
		('77f88764-d894-4d84-8c1e-08f01012d030', 'How much do you make in a calendar year?', '2019-11-22 01:46:52', -1, 3, 1, 0, 4);


	INSERT INTO [dbo].[SurveyChoices] ([Id], [QuestionId], [Text], [CreatedAtUtc], [OrderInQuestion]) 
	VALUES 
		--Teach
		('ff1804f5-566c-4c22-aa1c-b7e1f4fd6771', 'ff8cea86-6915-4eef-8a1e-de976525b149', '0 - 1 Years', '2019-10-29 17:58:39', 1),
		('cb7b6c9f-3e12-4f6f-848a-1dff5c8cfedc', 'ff8cea86-6915-4eef-8a1e-de976525b149', '2 - 5 Years', '2019-11-26 06:40:20', 2),
		('470d0c6b-4e75-4cf9-ad66-5d6da11f48c7', 'ff8cea86-6915-4eef-8a1e-de976525b149', '5 - 10 Years', '2019-04-09 11:52:41', 3),
		('470d0c6b-4e75-4cf9-ad66-5d6da11f48c1', 'ff8cea86-6915-4eef-8a1e-de976525b149', '10+ Years', '2019-04-09 11:52:41', 4),

		('88e455ac-29b3-4469-8a08-143c2a07caa8', '931e7eb4-dc93-4b7b-8d9f-c030856cff03', 'Yes', '2019-11-02 01:56:30', 1),
		('1fd5793b-0dc7-40c2-bd37-f3a9441f3da9', '931e7eb4-dc93-4b7b-8d9f-c030856cff03', 'No', '2019-05-20 01:33:33', 2),
		
		--Practice
		('88e455ac-29b3-9876-8a08-143c2a07caa8', '93333504-8807-4ca1-b17b-9d4440d1a352', 'Yes', '2019-11-02 01:56:30', 1),
		('1fd5793b-0dc7-9876-bd37-f3a9441f3da9', '93333504-8807-4ca1-b17b-9d4440d1a352', 'No', '2019-05-20 01:33:33', 2),	

		('2afcdff6-c670-4232-8b88-cff77665d179', 'eef07c54-4329-48c4-88fa-fc7c7cf36da0', 'Yes', '2019-11-02 01:56:30', 1),
		('276b8a98-1278-47ef-af0b-aa3df2dacbe2', 'eef07c54-4329-48c4-88fa-fc7c7cf36da0', 'No', '2019-05-20 01:33:33', 2),	

		--Other
		('2d49e166-590f-4618-b33f-2fb0c0d7c446', '82fb6add-449d-4758-a97e-33731437928b', 'Yes', '2019-11-02 01:56:30', 1),
		('673e57ce-130c-40e2-97be-e9e47191eaca', '82fb6add-449d-4758-a97e-33731437928b', 'No', '2019-05-20 01:33:33', 2),	
		

		--For the default questions: Gender, ethnicity, degree, salary
		('edb2b15e-fff6-4acb-ab50-dcf9a1e410e1', 'C4A198DD-D450-4DF5-A086-CE2AEDE6B643', 'Male', '2019-10-29 17:58:39', 1),
		('b6efb993-6b28-402a-8d3d-4e4b639e2f78', 'C4A198DD-D450-4DF5-A086-CE2AEDE6B643', 'Female', '2019-11-26 06:40:20', 2),
		('4b32367a-e0fb-476c-92f1-9532e4c9a230', 'C4A198DD-D450-4DF5-A086-CE2AEDE6B643', 'Other', '2019-04-09 11:52:41', 3),
		('d2c0f60b-7a88-4338-a749-e3ef7870e2be', 'C4A198DD-D450-4DF5-A086-CE2AEDE6B643', 'Prefer not to say', '2019-04-09 11:52:41', 4),

		('06155832-e831-402e-80eb-f7c630142815', '54E8941B-4901-469B-950F-DD1D97620BDA', 'White', '2019-10-29 17:58:39', 1),
		('b6efb993-6b28-402a-8d3d-4e4b939e2f78', '54E8941B-4901-469B-950F-DD1D97620BDA', 'African American', '2019-11-26 06:40:20', 2),
		('112b6762-1224-4f87-a14d-8d90df8a859c', '54E8941B-4901-469B-950F-DD1D97620BDA', 'American Indian or Alaska Native', '2019-04-09 11:52:41', 3),
		('79f04d71-7140-4601-a56c-0161d724d564', '54E8941B-4901-469B-950F-DD1D97620BDA', 'Native Hawaiian or Other Pacific Islander', '2019-04-09 11:52:41', 4),
		('a6e6ec93-d86e-4e26-b063-3215a17e470e', '54E8941B-4901-469B-950F-DD1D97620BDA', 'Hispanic, Latino, or Spanish', '2019-10-29 17:58:39', 5),
		('facccfa2-6215-4edb-a021-9b9f52f03e20', '54E8941B-4901-469B-950F-DD1D97620BDA', 'Other', '2019-04-09 11:52:41', 6),
		('6d440321-a8ad-41ec-8f8d-d4c59378e2f3', '54E8941B-4901-469B-950F-DD1D97620BDA', 'Prefer not to say', '2019-04-09 11:52:41', 7),

		('e9c42fcb-2871-4956-85a8-dbe6be4e7325', 'DED673D6-960C-4C59-A906-02CB7253D0EA', 'High school diploma or equivalency (GED)', '2019-10-29 17:58:39', 1),
		('1c49ba67-7c63-4d34-bf57-339e1d12851b', 'DED673D6-960C-4C59-A906-02CB7253D0EA', 'Associate degree (junior college)', '2019-11-26 06:40:20', 2),
		('11f1453e-642a-4a3d-a461-cea2732134f9', 'DED673D6-960C-4C59-A906-02CB7253D0EA', 'Bachelors degree', '2019-04-09 11:52:41', 3),
		('439a2d3a-7762-4e8e-b8e9-a5d28525ffe3', 'DED673D6-960C-4C59-A906-02CB7253D0EA', 'Masters degree', '2019-04-09 11:52:41', 4),
		('0b853daa-c9f0-4fb2-ab66-c56a0a8ec0fe', 'DED673D6-960C-4C59-A906-02CB7253D0EA', 'Doctorate', '2019-10-29 17:58:39', 5),
		('b47d9d6c-aa99-4480-9423-9f17b62c5a30', 'DED673D6-960C-4C59-A906-02CB7253D0EA', 'Professional (MD, JD, DDS, etc.)', '2019-04-09 11:52:41', 6),
		('6cd0a039-25ea-4431-a864-c94c32562e35', 'DED673D6-960C-4C59-A906-02CB7253D0EA', 'Prefer not to say', '2019-04-09 11:52:41', 7),

		('35cd2d4f-88c9-49a0-a90d-1533083ddaee', '77f88764-d894-4d84-8c1e-08f01012d030', '$0 - $19,999', '2019-04-09 11:52:41', 1),
		('04d02831-80f3-465e-a082-3e1bb2c0ac23', '77f88764-d894-4d84-8c1e-08f01012d030', '$20,000 - $49,999', '2019-04-09 11:52:41', 2),
		('d16c99e2-3af3-423e-bf7f-f58a28975f4e', '77f88764-d894-4d84-8c1e-08f01012d030', '$50,000 - $99,999', '2019-10-29 17:58:39', 3),
		('da434114-b1fc-4c0b-956e-381e28ed777b', '77f88764-d894-4d84-8c1e-08f01012d030', '$100,000+', '2019-04-09 11:52:41', 4),
		('7b77a5f0-72d3-4a62-bb43-fafdf65244a0', '77f88764-d894-4d84-8c1e-08f01012d030', 'Prefer not to say', '2019-04-09 11:52:41', 5);


	INSERT INTO [dbo].[SurveyAnswers] ([Id], [UserId], [QuestionId], [SubmittedAtUtc], [Answer], [UserTypeId]) 
	VALUES 
		--Answers to be deleted but are here for reporting demo
		('11378ba8-0d00-43eb-a4b2-75a3a709fa82', '5b9d5b5d-8c43-4d1b-9d8e-f05f540aaf5d', '736f41a8-5f74-4be7-9956-c1250639f768', '2019-08-24 15:32:07', 'Ipsum sum.', 1),
		('2453cc70-086b-469d-9cd0-eeb62a5e601c', 'e6343b15-9b67-48fb-8b82-7de1b0a94bf0', 'ff8cea86-6915-4eef-8a1e-de976525b149', '2019-12-20 04:58:25', 'Integer a nibh.', 1),
		('b7508f6d-63c4-467a-8042-efb0af398ea1', '54fa1356-fa06-405b-82f3-6887cf391e12', '931e7eb4-dc93-4b7b-8d9f-c030856cff03', '2019-07-30 22:08:43', 'Fusce congue, diam id ornare imperdiet, sapien urna pretium nisl, ut volutpat sapien arcu sed augue. Aliquam erat volutpat.', 3),
		('c30d1e95-9ac6-4ea3-8cf8-7497b042bb6a', '9eef31f5-c364-44b9-ada0-706bcb809372', 'd4104646-e91a-434a-985b-b1c5ca10b05d', '2019-12-24 16:37:29', 'Morbi vestibulum, velit id pretium iaculis, diam erat fermentum justo, nec condimentum neque sapien placerat ante.', 3),
		('d8dead6c-bf5f-4a89-8c5f-49a026d50c80', '875ae519-2e88-4d50-ae2a-43443c2bbbb6', 'f36a4cb2-8497-48e9-a279-d7ae3d19a6dc', '2019-08-10 02:20:33', 'Duis bibendum, felis sed interdum venenatis, turpis enim blandit mi, in porttitor pede justo eu massa. Donec dapibus.', 4),
		('61a22368-e23a-4a78-9c83-9cae203320dd', '5700af35-76d1-4fc4-9584-74de887c6c17', 'eef07c54-4329-48c4-88fa-fc7c7cf36da0', '2019-08-15 02:37:22', 'Nunc nisl.', 2),
		('787c2975-a63c-4dba-97cb-cbee842edc50', 'c0fb2480-9e6b-48da-ad59-6a0dbce3e13c', '26ed365f-f10c-4cfa-9d38-7499cdad8d9e', '2019-12-04 02:45:37', 'Morbi ut odio. Cras mi pede, malesuada in, imperdiet et, commodo vulputate, justo.', 2);


	INSERT INTO [dbo].[SurveyFeedback] ([Id], [Text], [SubmittedAtUtc], [Rating], [UserId], [Url]) 
	VALUES 
		--Feedback to be deleted but is here for reporting demo
		('c0d1be73-3436-4ffe-9ac7-10bcd78f63ea', 'This page needs more pictures!', '2019-08-08 04:20:53', 4, '9fe7c798-e66b-45e3-8f66-dbedc046fdd8', 'https://www.y4c.org/new-page-4'),
		('17532e67-9f11-49f2-baa2-4a4307f23171', 'None', '2018-12-23 08:04:47', 4, '5ae0c99d-aaf7-4a59-9eeb-240747d6f9a5', 'https://www.y4c.org/new-page-4'),
		('8e756611-1a61-4c88-b15f-0f9a04ea0a76', 'I would like to learn more about the opportunities available', '2018-12-23 22:47:58', 1, 'ea2e9c87-0e00-45ae-9945-ddb84b944a64', 'https://www.y4c.org/new-page-4'),
		('35453836-236b-457e-b0c4-903296fd4bd2', 'Nothing to add. Great read!', '2018-10-06 12:09:05', 4, 'f96f8aea-ff73-4075-81a7-97236a68566c', 'https://www.y4c.org/new-page-4'),
		('e7545d9f-f4ab-4495-a21f-3a0ec92ea714', 'This was not what I was looking for at all!', '2019-07-24 23:32:11', 3, '84f7d8be-7c9b-4f93-ade3-9066c87da9ee', 'https://www.y4c.org/teacher-page'),
		('f251dedd-5417-4938-922a-6bbe56b4c961', 'Not useful to what I was after.', '2018-12-19 03:27:40', 2, '3bf227f0-66b8-42a0-9875-3a33797bbf31', 'https://www.y4c.org/teacher-page'),
		('94863ec6-c97d-4339-ac9f-b2ec4a435701', 'I did not like that you asked personal information', '2018-09-24 01:33:54', 1, '70439970-b973-42e6-8775-1f37941b06f2', 'https://www.y4c.org/new-page-4'),
		('7c4dcd97-5b74-4fa8-9c40-e0d0cf4a89a7', 'Looking great! Thanks for this survey!', '2019-12-17 21:14:17', 5, 'c019906e-9c3c-4fd3-b033-5c7a748ae0a3', 'https://www.y4c.org/volunteer-1'),
		('0e35f1b5-12f2-4f55-8451-500fb69b44b9', 'Nothing to add.', '2018-10-09 07:08:07', 5, '464deb3b-cd25-4757-8e13-7f070c04e43a', 'https://www.y4c.org/volunteer-1'),
		('89cf82f1-5cdc-445d-ab95-8d5b279f3e8a', 'Well put together survey and resulting page.', '2020-01-10 01:08:55', 5, '20ccd95b-9bfc-413c-9b08-829bfab7be7b', 'https://www.y4c.org/teacher-page');


	INSERT INTO [dbo].[UrlToVisitors] ([Id], [Url], [UserType], [Category], [Weight], [CreatedAtUtc], [LastModifiedAtUtc]) 
	VALUES 
		--Teach branch
		('7fd775df-5a7c-4279-a9c2-139df691da3d', 'https://www.y4c.org/teacher-page', 2, 1, 1, '2020-08-08 04:20:53', '2020-08-08 04:20:53'),
		('f876bb29-7329-4060-adf2-5f085d231344', 'https://www.y4c.org/donate', 3, 1, 0, '2020-08-08 04:20:53', '2020-08-08 04:20:53'),

		--Practice branch
		('c2a83e4a-a290-43e5-93e7-224c1a91f47f', 'https://www.y4c.org/new-page-4', 4, 2, 1, '2020-08-08 04:20:53', '2020-08-08 04:20:53'),
		('f5c2aa96-8a18-4582-a424-53bcef7a7723', 'https://www.y4c.org/donate', 3, 2, 0, '2020-08-08 04:20:53', '2020-08-08 04:20:53'),

		--Other branch
		('6c840c44-5c68-4d81-871c-3633c3ce2630', 'https://www.y4c.org/volunteer-1', 1, 3, 1, '2020-08-08 04:20:53', '2020-08-08 04:20:53'),
		('a4755ed3-253f-4798-9681-7cf3f2847f19', 'https://www.y4c.org/donate', 3, 3, 0, '2020-08-08 04:20:53', '2020-08-08 04:20:53');
	END
END