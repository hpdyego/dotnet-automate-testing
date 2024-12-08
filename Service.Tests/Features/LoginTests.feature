Feature: Login Functionality

  Scenario: Successful Login
    Given I have navigated to the login page
    When I enter "0" as username and "HML@Gera!" as password
    And I click the login button
    Then I should see the dashboard

  Scenario: Failed Login
    Given I have navigated to the login page
    When I enter "0" as username and "HML@Gera!" as password
    And I click the login button
    Then I should see an error message "Invalid credentials"
