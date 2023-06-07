Feature: CreateProductTest
	Create a product

@positive @smoke @regression @integration @JIRA-1
Scenario: Add a new product
	Given the user password to product
	And the token with a valid value to product
	Then Then the result status code should be 200
	And  the user creates a new product with the following information
		| name           | description    | image              | price | category |
		| Purple Glasses | Purple Glasses | purple-glasses.jpg | 19.99 | 7        |
  	Then Then the result status code should be 200
  	And  The user sees the proper product information created
  	