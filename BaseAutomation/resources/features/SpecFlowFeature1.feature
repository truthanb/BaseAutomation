@setupFeature
Feature: SpecFlowFeature1
	Framework testing

Scenario: Add two numbers
	Given I have gone to heroku app
	Then the result should be 120 on the screen
