import numpy as np 
import os

from scipy import ndimage as ndi
import scipy
from skimage.color import rgb2gray
from skimage import feature
from skimage.filters import roberts, sobel, scharr, prewitt

rootDir = "D:\\Temp\\Upload\\"
images = []

for root, dirs, files in os.walk(rootDir + "Albums"):  
    for filename in files:
        if  '.txt' not in filename:
            imagePath = root + "\\" + filename #os.path.join(root, filename)
            images.append(imagePath)    
      

for image in images:    
    im = scipy.misc.imread(image)

    img_gray = rgb2gray(im) 

    edge_roberts = roberts(img_gray)
    edge_sobel = sobel(img_gray)
    edge_canny1 = feature.canny(img_gray)
    edge_canny2 = feature.canny(img_gray, sigma=0.2)
    edge_canny3 = feature.canny(img_gray, sigma=0.25)
    edge_canny4 = feature.canny(img_gray, sigma=0.3)
    edge_prewitt = prewitt(img_gray)

    scipy.misc.imsave(rootDir + "Processed\\GrayScale\\" + image.split('\\')[-1] , img_gray)
    scipy.misc.imsave(rootDir + "Processed\\Roberts\\" + image.split('\\')[-1] , edge_roberts)
    scipy.misc.imsave(rootDir + "Processed\\sobel\\" + image.split('\\')[-1] , edge_sobel)
    scipy.misc.imsave(rootDir + "Processed\\Canny\\" + image.split('\\')[-1] , edge_canny1)
    scipy.misc.imsave(rootDir + "Processed\\Canny0.2\\" + image.split('\\')[-1] , edge_canny2)
    scipy.misc.imsave(rootDir + "Processed\\Canny0.25\\" + image.split('\\')[-1] , edge_canny3)
    scipy.misc.imsave(rootDir + "Processed\\Canny0.3\\" + image.split('\\')[-1] , edge_canny4)
    scipy.misc.imsave(rootDir + "Processed\\Prewitt\\" + image.split('\\')[-1] , edge_prewitt)
    





    