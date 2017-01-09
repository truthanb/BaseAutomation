@setupFeature
Feature: Feature.feature
	Framework testing

Scenario: Basic framework check. Login to http://the-internet.herokuapp.com/login 
	Given Login to heroku app
	Then Watch time go by