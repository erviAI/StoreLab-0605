# RetroStore by StoreLab

The current console application is outdated and needs improvements. The following steps outline the process, starting with enhancements to the existing app and later expanding to a web solution.

## Step 1: Enhance the Console Application
- Extend the data model with additional properties (Model/UI).
- Add support for multiple payment types:
    - Introduce a payment direction: ["In", "Out"].
    - Restrict cash payments from being processed via Vipps/Swish.
- Improve search functionality:
    - Enable searching by various columns and by price (advanced search).

## Step 2: Test and Validation
- Ensure all new features and changes work as intended.

## Step 3: WebShop Expansion
- Build a web API leveraging StoreLab's core functionality.
- Develop an attractive frontend using Next.js, allowing users to:
    - Browse the catalog with images.
    - Add items to a basket.
    - Complete purchases through a checkout process.
