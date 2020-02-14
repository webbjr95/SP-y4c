BEGIN
	IF NOT EXISTS (SELECT * FROM SurveyQuestions)
	BEGIN
		INSERT INTO SurveyQuestions(QuestionNumber, QuestionTypeId, [Text])
		VALUES
			(1, 4, 'Do you teach yoga?'),
			(2, 4, 'How long have you been teaching?'),
			(3, 1, 'Where do you teach?'),
			(4, 4, 'Are you interested in teaching or volunteering at Yoga 4 Change?'),
			(5, 4, 'Do you practice yoga?'),
			(6, 4, 'Have you practiced with Yoga 4 Change before?'),
			(7, 1, 'Where have you practiced?'),
			(8, 4, 'Are you interested in getting involved?')
	END
END