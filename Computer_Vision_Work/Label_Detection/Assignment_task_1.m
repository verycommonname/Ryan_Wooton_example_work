function [ output_args ] = Assignment_task_1( InputImage )

I = imread(InputImage);%'E:\College-Uni work\Master (MSc)\Computer vision\TopImg0000.bmp'
subplot(2,2,1); imshow(I); title('Original Image');
% Here the program read the image and readies it to be displayed.

BW = im2bw(I,0.01); % The program converts the image into a binary copy 
                    % with a specified limit.
BWFill = imfill(BW, 'holes'); % The program fills in a number of holes 
                              % within the whitespace of the image.
BW2 = bwareaopen(BWFill, 100); % The program eliminates any noise by
                               % removing any pixel cluster smaller that
                               % the specified amount.
subplot(2,2,2); imshow(BW2); title('Binary Image'); % The resulting mask is
                                                    % readied to be
                                                    % displayed.

horizontal = any(BW2, 1);
vertical = any(BW2, 2);
% Above the program looks through the mask, noting the co-ordinates of each
% white pixel in each row and column

column1 = find(horizontal, 1, 'first');
column2 = find(horizontal, 1, 'last');
row1 = find(vertical, 1, 'first');
row2 = find(vertical, 1, 'last');
% Above the program looks through the results of the row and column searchs
% and finds the first and last value of each to be used as the co-ordinates
% for where to crop the image.

CroppedI = I(row1:row2, column1:column2); % The image is cropped using the
                                          % the mask.
subplot(2,2,3); imshow(CroppedI); title('Segmented Tray Image');
% Above the cropped image is readied to be displayed.

% The process commented above is repeated again to find the label, with the
% only different being modifications to the limits of the 'im2bw' and
% 'bwareaopen' functions.
CropBW = im2bw(CroppedI,0.4);
CropBWFill = imfill(CropBW, 'holes');
CropBW2 = bwareaopen(CropBWFill, 15000);

Crophorizontal = any(CropBW2, 1);
Cropvertical = any(CropBW2, 2);

CropColumn1 = find(Crophorizontal, 1, 'first');
CropColumn2 = find(Crophorizontal, 1, 'last');
CropRow1 = find(Cropvertical, 1, 'first');
CropRow2 = find(Cropvertical, 1, 'last');

FinalI = CroppedI(CropRow1:CropRow2, CropColumn1:CropColumn2);
subplot(2,2,4); imshow(FinalI); title('Segmented Label Image');
% Above the final image is readied and all of the images are displayed.
end
