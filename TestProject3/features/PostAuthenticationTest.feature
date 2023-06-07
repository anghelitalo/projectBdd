Feature: PostAuthenticationTest
	Get the token authentication

@positive @smoke @regression @integration @JIRA-0
Scenario: Authentication to api gatling
	Given the user password
	And the token with a valid value
	Then the result status code should be 200