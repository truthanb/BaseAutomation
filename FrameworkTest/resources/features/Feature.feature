@setupFeature
Feature: Feature.feature
	Framework testing

Scenario: Basic framework check. Login to http://the-internet.herokuapp.com/login 
	Given Login to heroku app
	| UserName | Password             |
	| tomsmith | SuperSecretPassword! |
	Then Verify successful login

Scenario: Fail for demo
	Given Login to heroku app
	| UserName | Password             |
	| tomsmith | SuperSecretPassword! |
	Then Fail Intentionally