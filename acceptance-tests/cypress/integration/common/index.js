/// <reference types="Cypress" />
import { Given, Then } from "cypress-cucumber-preprocessor/steps";

Given('user is navigating to {string}', (a) => {
    cy.visit(a);
});

Then('the page title is {string}', (title) => {
    cy.get('h1').should('have.text', title);
});

Then('the HTML is displayed', () => {
    cy.get('body div dl').as('container');
    cy.get('@container').find('dt').should('be.visible');
    cy.get('@container').find('dd').should('be.visible');
});

Then('term description is {string}', (defText) => {
    cy.get('.definition dl dd').should('have.text', defText);
});

Then('user is redirected to {string} on cancer.gov', (url) => {
    cy.location('host').should('eq', 'www.cancer.gov');
    cy.location('pathname').should('eq', url)
});

Then('user is navigating to error page {string}', (url) => {
    cy.visit(url, { failOnStatusCode: false });
});

Then('the error page title is {string}', (title) => {
    cy.get('.error-content-english h1').should('have.text', title);
});

And('the title tag should be {string}', (title) => {
    cy.get('head > title').should('have.text', title);
});

And('{string} metatag exists is the data for the page with content {string}', (name, content) => {
    cy.get(`head meta[name="${name}"]`).should('have.attr', 'content', content);
});