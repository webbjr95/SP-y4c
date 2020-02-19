BEGIN
	INSERT INTO [dbo].[SurveyQuestions] ([Id], [Text], [CreatedAtUtc], [QuestionNumber], [TypeId], [ActiveStatus], [Weight]) 
	VALUES 
		--First is for teaching, second is for practice, third is other, and lastly the socio-economic questions.
		('736f41a8-5f74-4be7-9956-c1250639f768', 'If you teach, where at?', '2019-10-29 17:58:39', 1, 1, 1, 0),
		('ff8cea86-6915-4eef-8a1e-de976525b149', 'How long have you been teaching yoga?', '2019-11-26 06:40:20', 2, 3, 1, 0),
		('931e7eb4-dc93-4b7b-8d9f-c030856cff03', 'Would you be interested in becoming a teacher for Yoga 4 Change?', '2019-04-09 11:52:41', 3, 3, 1, 1),

		('d4104646-e91a-434a-985b-b1c5ca10b05d', 'If you practice, where at?', '2019-11-02 01:56:30', 4, 1, 1, 0),
		('f36a4cb2-8497-48e9-a279-d7ae3d19a6dc', 'Do you practice at a commercial facility or within a community group?', '2019-05-20 01:33:33', 5, 3, 1, 0),
		('eef07c54-4329-48c4-88fa-fc7c7cf36da0', 'Would you be interested in practicing at a Yoga 4 Change facility?', '2019-02-10 21:49:07', 6, 3, 1, 1),

		('26ed365f-f10c-4cfa-9d38-7499cdad8d9e', 'Would you like to volunteer?', '2019-04-11 21:58:48', 7, 3, 1, 1),
		('69ee15ec-02e9-4d0e-b3cb-a0038f1b3edf', 'Where did you hear about us?', '2019-11-22 01:46:52', 8, 1, 1, 0),
		('69ee15ec-02e9-4d0e-b3cb-a0038f1b3ed1', 'test checkbox?', '2019-11-22 01:46:52', 8, 2, 1, 0);

	INSERT INTO [dbo].[SurveyChoices] ([Id], [QuestionId], [Text], [CreatedAtUtc]) 
	VALUES 
		('ff1804f5-566c-4c22-aa1c-b7e1f4fd6771', 'ff8cea86-6915-4eef-8a1e-de976525b149', '0 - 1 Years', '2019-10-29 17:58:39'),
		('cb7b6c9f-3e12-4f6f-848a-1dff5c8cfedc', 'ff8cea86-6915-4eef-8a1e-de976525b149', '2 - 5 Years', '2019-11-26 06:40:20'),
		('470d0c6b-4e75-4cf9-ad66-5d6da11f48c7', 'ff8cea86-6915-4eef-8a1e-de976525b149', '5 - 10 Years', '2019-04-09 11:52:41'),
		('470d0c6b-4e75-4cf9-ad66-5d6da11f48c1', 'ff8cea86-6915-4eef-8a1e-de976525b149', '10+ Years', '2019-04-09 11:52:41'),

		('88e455ac-29b3-4469-8a08-143c2a07caa8', '931e7eb4-dc93-4b7b-8d9f-c030856cff03', 'Yes', '2019-11-02 01:56:30'),
		('1fd5793b-0dc7-40c2-bd37-f3a9441f3da9', '931e7eb4-dc93-4b7b-8d9f-c030856cff03', 'No', '2019-05-20 01:33:33');

	INSERT INTO [dbo].[SurveyAnswers] ([Id], [UserId], [QuestionId], [SubmittedAtUtc], [Answer], [UserTypeId]) 
	VALUES 
	('11378ba8-0d00-43eb-a4b2-75a3a709fa82', '5b9d5b5d-8c43-4d1b-9d8e-f05f540aaf5d', '736f41a8-5f74-4be7-9956-c1250639f768', '2019-08-24 15:32:07', 'Ipsum sum.', 1),
	('2453cc70-086b-469d-9cd0-eeb62a5e601c', 'e6343b15-9b67-48fb-8b82-7de1b0a94bf0', 'ff8cea86-6915-4eef-8a1e-de976525b149', '2019-12-20 04:58:25', 'Integer a nibh.', 1),
	('b7508f6d-63c4-467a-8042-efb0af398ea1', '54fa1356-fa06-405b-82f3-6887cf391e12', '931e7eb4-dc93-4b7b-8d9f-c030856cff03', '2019-07-30 22:08:43', 'Fusce congue, diam id ornare imperdiet, sapien urna pretium nisl, ut volutpat sapien arcu sed augue. Aliquam erat volutpat.', 3),
	('c30d1e95-9ac6-4ea3-8cf8-7497b042bb6a', '9eef31f5-c364-44b9-ada0-706bcb809372', 'd4104646-e91a-434a-985b-b1c5ca10b05d', '2019-12-24 16:37:29', 'Morbi vestibulum, velit id pretium iaculis, diam erat fermentum justo, nec condimentum neque sapien placerat ante.', 3),
	('d8dead6c-bf5f-4a89-8c5f-49a026d50c80', '875ae519-2e88-4d50-ae2a-43443c2bbbb6', 'f36a4cb2-8497-48e9-a279-d7ae3d19a6dc', '2019-08-10 02:20:33', 'Duis bibendum, felis sed interdum venenatis, turpis enim blandit mi, in porttitor pede justo eu massa. Donec dapibus.', 4),
	('61a22368-e23a-4a78-9c83-9cae203320dd', '5700af35-76d1-4fc4-9584-74de887c6c17', 'eef07c54-4329-48c4-88fa-fc7c7cf36da0', '2019-08-15 02:37:22', 'Nunc nisl.', 2),
	('787c2975-a63c-4dba-97cb-cbee842edc50', 'c0fb2480-9e6b-48da-ad59-6a0dbce3e13c', '26ed365f-f10c-4cfa-9d38-7499cdad8d9e', '2019-12-04 02:45:37', 'Morbi ut odio. Cras mi pede, malesuada in, imperdiet et, commodo vulputate, justo.', 2);

	INSERT INTO [dbo].[SurveyFeedback] ([Id], [SubmittedAtUtc], [Rating], [UserId], [Url]) 
	VALUES 
		('c0d1be73-3436-4ffe-9ac7-10bcd78f63ea', '2019-08-08 04:20:53', 4, '9fe7c798-e66b-45e3-8f66-dbedc046fdd8', 'https://www.y4c.org/new-page-4'),
		('17532e67-9f11-49f2-baa2-4a4307f23171', '2018-12-23 08:04:47', 4, '5ae0c99d-aaf7-4a59-9eeb-240747d6f9a5', 'https://www.y4c.org/new-page-4'),
		('8e756611-1a61-4c88-b15f-0f9a04ea0a76', '2018-12-23 22:47:58', 1, 'ea2e9c87-0e00-45ae-9945-ddb84b944a64', 'https://www.y4c.org/new-page-4'),
		('35453836-236b-457e-b0c4-903296fd4bd2', '2018-10-06 12:09:05', 4, 'f96f8aea-ff73-4075-81a7-97236a68566c', 'https://www.y4c.org/new-page-4'),
		('e7545d9f-f4ab-4495-a21f-3a0ec92ea714', '2019-07-24 23:32:11', 3, '84f7d8be-7c9b-4f93-ade3-9066c87da9ee', 'https://www.y4c.org/teacher-page'),
		('f251dedd-5417-4938-922a-6bbe56b4c961', '2018-12-19 03:27:40', 2, '3bf227f0-66b8-42a0-9875-3a33797bbf31', 'https://www.y4c.org/teacher-page'),
		('94863ec6-c97d-4339-ac9f-b2ec4a435701', '2018-09-24 01:33:54', 1, '70439970-b973-42e6-8775-1f37941b06f2', 'https://www.y4c.org/new-page-4'),
		('7c4dcd97-5b74-4fa8-9c40-e0d0cf4a89a7', '2019-12-17 21:14:17', 5, 'c019906e-9c3c-4fd3-b033-5c7a748ae0a3', 'https://www.y4c.org/volunteer-1'),
		('0e35f1b5-12f2-4f55-8451-500fb69b44b9', '2018-10-09 07:08:07', 5, '464deb3b-cd25-4757-8e13-7f070c04e43a', 'https://www.y4c.org/volunteer-1'),
		('89cf82f1-5cdc-445d-ab95-8d5b279f3e8a', '2020-01-10 01:08:55', 5, '20ccd95b-9bfc-413c-9b08-829bfab7be7b', 'https://www.y4c.org/teacher-page')
END