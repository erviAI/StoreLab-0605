# Example Prompts for Project Documentation and Analysis

[Back to outline](../outline.md)

Goal:
- Review the existing codebase, understand the architecture, and document the main components and their responsibilities.


### EX 1: Document and analyse the current application

* `Describe the overall project and its purpose`
  ```
  @workspace Describe this project
  ```

* `Describe the ConsoleApp class and its responsibilities`
  ```
  @workspace Describe the ConsoleApp class
  ```

* `Describe the Program.cs file and its main logic`
  ```
  @workspace Describe the Program.cs file
  ```

* `Explain how the data access classes function`
  ```
  @workspace /explain how the data access classes work
  ```

### Ex 2: Create the project documentation for the README file

* `Prompt for a decent readme file`
  ```
  @workspace Generate the contents of a README.md file for the code repository. Use "RetroStore by StoreLab" as the project title. The README file should include the following sections: Description, Project Structure, Key Classes and Interfaces, Usage, License. Format all sections as raw markdown. Use a bullet list with indents to represent the project structure. Do not include ".gitignore" or the ".github", "bin", "docs" and "obj" folders.
  ```

### Ex 3: Commit

Use "instructions" for commit messages settings.json 

---

## More prompts

- What are the main components and responsibilities of the backend and frontend?
- Draw a high-level architecture diagram of the solution.
- List and describe the main models/entities in the application.
- What are the main workflows (e.g., selling, payment, basket management)?
- Identify any technical debt or areas for improvement.
- What are the dependencies between modules?
- How is data persisted and retrieved?
- What are the main entry points for the application?


## Summary

This document provides a set of example prompts to help users describe and analyze the current application. The prompts are designed to facilitate project documentation, code exploration, and understanding of key components such as classes, files, and data access logic. Use these prompts as a starting point for generating detailed documentation or for onboarding new team members.