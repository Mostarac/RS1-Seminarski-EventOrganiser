	//console.log(document.querySelector('.skyDIV').clientHeight);
	//console.log(document.querySelector('.skyDIV').clientWidth);

window.addEventListener('scroll', function () {
	var ypos = window.pageYOffset;
	//console.log(ypos);
	var modifierWidth = ypos  * (1366 / document.querySelector('.skyDIV').clientWidth);
	var modifierHeight = ypos  * (2397 / document.querySelector('.skyDIV').clientHeight);
	
	//Sun
	const Sun = document.querySelector('.sunIMG');
	Sun.style.transform = 'translate(0, ' + (50 + (modifierWidth/2.6)) + '%) rotate(' + ((modifierHeight*9)) + 'deg)';
	
	//Cloud4
	const Cloud4 = document.querySelector('.cloud4IMG');
	Cloud4.style.transform = 'translate(' + (0 + (-(modifierWidth/7))) + '%, ' + (0 + (modifierHeight/4)) + '%)';
	
	//Cloud5
	const Cloud5 = document.querySelector('.cloud5IMG');
	Cloud5.style.transform = 'translate(' + (0 + (modifierWidth/8)) + '%, ' + (0 + (modifierHeight/2.9)) + '%)';
	
	//Cloud2
	const Cloud2 = document.querySelector('.cloud2IMG');
	Cloud2.style.transform = 'translate(' + (30 + (modifierWidth/2)) + '%, ' + (20 + (modifierHeight/1.7)) + '%)';
	
	//Stage
	const Stage1 = document.querySelector('.wrapperIMG');
	Stage1.style.transform = 'translate(0, ' + (-145.02 + (modifierHeight/16)) + '%)';
});